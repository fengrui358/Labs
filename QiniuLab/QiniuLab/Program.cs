using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.FileProviders;
using Qiniu.Conf;
using Qiniu.IO;
using Qiniu.IO.Resumable;
using Qiniu.RPC;
using Qiniu.RS;

namespace QiniuLab
{
    class Program
    {
        static void Main(string[] args)
        {
            const string domain = "http://obftsogus.bkt.clouddn.com/";
            //设置上传的空间
            String bucket = "test2";

            Console.WriteLine("初始化");
            Config.Init();

            #region 上传

            Console.WriteLine("上传测试，按键继续……");
            //Console.ReadLine();

            IOClient target = new IOClient();
            PutExtra extra = new PutExtra();

            //设置上传的文件的key值
            String key = string.Format("{0}.JPG", DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));

            //普通上传,只需要设置上传的空间名就可以了,第二个参数可以设定token过期时间
            PutPolicy put = new PutPolicy(bucket, 3600);

            //调用Token()方法生成上传的Token
            string upToken = put.Token();
            Console.WriteLine("upToken：" + upToken);

            //上传文件的路径
            String filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", "测试图片.JPG");
            var bytes = File.ReadAllBytes(filePath);

            var url = string.Empty;
            using (var stream = new MemoryStream(bytes))
            {
                //调用PutFile()方法上传
                PutRet ret = target.PutAsync(upToken, key, stream, extra).Result;

                //打印出相应的信息
                Console.WriteLine(ret.Response);

                if (ret.OK)
                {
                    url = string.Concat(domain, ret.key);
                    Console.WriteLine("上传成功");
                }
                else
                {
                    Console.WriteLine("上传失败");
                }
                
                Console.WriteLine(ret.key);
            }
            
            //Console.ReadLine();

            #endregion

            #region 获取文件信息

            Console.WriteLine("获取基本信息测试，按键继续……");
            //Console.ReadLine();

            //实例化一个RSClient对象，用于操作BucketManager里面的方法
            RSClient client = new RSClient();
            //调用Stat方法获取文件的信息
            Entry entry = client.StatAsync(new EntryPath(bucket, key)).Result;
            if (entry.OK)
            {
                Console.WriteLine("获取基本信息成功");

                //打印文件的hash、fsize等信息
                Console.WriteLine("Hash: " + entry.Hash);
                Console.WriteLine("Fsize: " + entry.Fsize);
                Console.WriteLine("PutTime: " + entry.PutTime);
                Console.WriteLine("MimeType: " + entry.MimeType);
                //Console.ReadLine();
            }
            else
            {
                Console.WriteLine("获取基本信息失败");
            }

            #endregion

            #region 下载文件

            Console.WriteLine("下载文件测试，按键继续……");
            //Console.ReadLine();

            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync(url))
                {
                    if (response.Result.IsSuccessStatusCode)
                    {
                        var s = response.Result.Content.ReadAsByteArrayAsync().Result;

                        var downLoadPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                            string.Concat(Guid.NewGuid().ToString("N"), ".jpg"));
                        File.WriteAllBytes(downLoadPath, s);

                        if (File.Exists(downLoadPath))
                        {
                            Console.WriteLine("下载成功，路径：" + downLoadPath);
                            Console.WriteLine("按任意键继续……");
                            //Console.ReadLine();

                            File.Delete(downLoadPath);
                        }
                        else
                        {
                            Console.WriteLine("下载失败");
                        }
                    }
                }
                
            }

            #endregion

            #region 删除单个文件

            Console.WriteLine("删除文件测试，按任意键继续……");
            //Console.ReadLine();

            //实例化一个RSClient对象，用于操作BucketManager里面的方法
            client = new RSClient();
            var ret2 = client.DeleteAsync(new EntryPath(bucket, key)).Result;
            if (ret2.OK)
            {
                Console.WriteLine("删除文件" + url + "成功");
            }
            else
            {
                Console.WriteLine("删除文件失败");
            }

            //Console.ReadLine();

            #endregion

            #region 上传视频并获取首帧(上传的视频在一天后删除)

            Console.WriteLine("上传视频并获取首帧(上传的视频在一天后删除)，按任意键继续……");
            //Console.ReadLine();

            put = new PutPolicy(bucket, 3600);
            put.DeleteAfterDays = 1;

            key = string.Format("视频-{0}.mov", DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
            //调用Token()方法生成上传的Token
            upToken = put.Token();
            Console.WriteLine("upToken：" + upToken);

            //上传文件的路径
            filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", "ios视频.mov");
            bytes = File.ReadAllBytes(filePath);

            url = string.Empty;
            using (var stream = new MemoryStream(bytes))
            {
                ResumablePutExtra putExtra = new ResumablePutExtra();
                putExtra.MimeType = "application/octet-stream";
                putExtra.Notify += new EventHandler<PutNotifyEvent>(extra_Notify);
                putExtra.NotifyErr += new EventHandler<PutNotifyErrorEvent>(extra_NotifyErr);
                var resumablePut = new ResumablePut(new Settings(), putExtra);

                //todo:视频文件较大，改为断点续传
                //调用PutFile()方法上传
                var ret3 = resumablePut.PutFileAsync(upToken, filePath, key).Result;
                
                //打印出相应的信息
                Console.WriteLine(ret3.Response);

                if (ret3.OK)
                {
                    //url = string.Concat(domain, ret3.key);
                    Console.WriteLine("上传成功");
                }
                else
                {
                    Console.WriteLine("上传失败");
                }

               // Console.WriteLine(ret3.key);
            }

            //Console.ReadLine();

            Console.WriteLine("触发持久化操作，获取首帧，按任意键继续……");
            //Console.ReadLine();

            //实例化一个entry对象
            var entryPath = new EntryPath(bucket, key);

            //设置转码操作参数
            String fops = "vframe/jpg/offset/1/w/480/h/360"; //"vframe/jpg/offset/1/w/480/h/360/rotate/90"; //"avthumb/mp4/s/640x360/vb/1.25m";
            //设置转码的队列
            String pipeline = "yourpipelinename";

            //可以对转码后的文件进行使用saveas参数自定义命名，当然也可以不指定文件会默认命名并保存在当前空间。
            String urlbase64 = Qiniu.Util.Base64URLSafe.Encode(string.Format("{0}:视频首页.jpg", domain));
            String pfops = fops;// + "|saveas/" + urlbase64;

            //实例化一个fop对象主要进行后续转码操作
            Qiniu.RS.Pfop fop = new Qiniu.RS.Pfop();

            Uri uri = new Uri("http://www.baidu.com");

            var s2 = fop.DoAsync(entryPath, new[] {pfops}, uri, null, 1).Result;
            Console.WriteLine(s2);
            //Console.ReadLine();

            #endregion
        }

        private static void extra_Notify(object sender, PutNotifyEvent e)
        {
            e.BlkIdx.ToString();
            e.BlkSize.ToString();
            Console.WriteLine(e.BlkIdx.ToString() + e.BlkSize.ToString());
        }

        private static void extra_NotifyErr(object sender, PutNotifyErrorEvent e)
        {
            e.BlkIdx.ToString();
            e.BlkSize.ToString();
        }
    }
}
