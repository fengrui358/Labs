using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.FileProviders;
using Qiniu.Conf;
using Qiniu.IO;
using Qiniu.RS;

namespace QiniuLab
{
    class Program
    {
        static void Main(string[] args)
        {
            const string domain = "http://obftsogus.bkt.clouddn.com/";

            Console.WriteLine("初始化");
            Qiniu.Conf.Config.Init();

            Console.WriteLine("上传测试，按键继续……");
            Console.ReadKey();

            IOClient target = new IOClient();
            PutExtra extra = new PutExtra();
            //设置上传的空间
            String bucket = "test2";
            //设置上传的文件的key值
            String key = string.Format("{0}.JPG", DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));

            //普通上传,只需要设置上传的空间名就可以了,第二个参数可以设定token过期时间
            PutPolicy put = new PutPolicy(bucket, 3600);

            //调用Token()方法生成上传的Token
            string upToken = put.Token();

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
                Console.WriteLine(ret.key);

                if (ret.OK)
                {
                    url = string.Concat(domain, ret.key);
                }
            }
            
            Console.ReadKey();
        }
    }
}
