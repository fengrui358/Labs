using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.FtpClient;
using System.Threading;
using WindowsFormsApplication1;

namespace FtpClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //var ftpClient = new System.Net.FtpClient.FtpClient();
            //ftpClient.Host = "192.168.1.80";
            //ftpClient.Port = 21;

            //ftpClient.

            var ftpClient = new System.Net.FtpClient.FtpClient();
            ftpClient.Host = "192.168.147.133";
            ftpClient.Port = 9904;
            ftpClient.Credentials = new NetworkCredential("kerry", "P@$$w0rd");
            ftpClient.UngracefullDisconnection = true;

            ftpClient.Connect();

            //var localFile = new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{Guid.NewGuid().ToString("N")}.text"));
            //using (var fs = localFile.Create())
            //{
            //    var content = Encoding.UTF8.GetBytes(Guid.NewGuid().ToString());
            //    fs.Write(content, 0, content.Length);
            //    fs.Flush();
            //}

            var localFile = new FileInfo(@"F:\Users\Administrator\Desktop\com.action98.drmp.mobilemonitor.apk");

            var remotePath = @"\A\com.action98.drmp.mobilemonitor.apk";

            //构建异步参数
            var asyncState = new Dictionary<string, object>();
            asyncState.Add("LocalPath", localFile.FullName);
            asyncState.Add("FtpClient", ftpClient);
            asyncState.Add("RemotePath", remotePath);

            //开始异步上传
            ftpClient.BeginOpenWrite(remotePath, FtpDataType.Binary, BeginOpenWriteCallback, asyncState);

            Console.ReadKey();
        }

        private static void BeginOpenWriteCallback(IAsyncResult ar)
        {
            var asyncState = ar.AsyncState as Dictionary<string, object>;
            var localPath = asyncState["LocalPath"].ToString();
            var ftpClient = asyncState["FtpClient"] as System.Net.FtpClient.FtpClient;
            var remotePath = asyncState["RemotePath"].ToString();

            var buf = new byte[1024 * 16];
            var readLength = 0;
            Stream istream = null, ostream = null;

            try
            {
                ostream = ftpClient.EndOpenWrite(ar);

                using (istream = new FileStream(localPath, FileMode.Open, FileAccess.Read))
                {
                    while ((readLength = istream.Read(buf, 0, buf.Length)) > 0)
                    {
                        ostream.Write(buf, 0, readLength);

                    }
                }

                ftpClient.Dispose();
                ostream.Dispose();
                

                //提前释放流，避免占用
                //DoFinnaly(ftpClient, ostream);
            }
            catch (Exception ex)
            {
                //DoFinnaly(ftpClient, ostream);

                //if (retryCount > _maxRetryCount)
                //{
                //    if (callBack != null)
                //    {
                //        callBack(false);
                //    }
                //}
                //else
                //{
                //    retryCount++;

                //    //LogHelper.WriteModuleLog<FtpClient>(string.Format("上传文件{0}失败，等待进行第{1}次尝试...", remotePath,
                //    //    retryCount));

                //    //休眠
                //    Thread.Sleep(retryCount * 1000);

                //    ////继续调用下载
                //    //BeginUploadFile_Inner(remotePath, localPath, callBack, notifyPercent, retryCount);
                //}
            }
        }
    }
}
