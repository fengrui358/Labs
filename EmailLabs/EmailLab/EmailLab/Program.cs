using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EmailLab
{
    class Program
    {
        static void Main(string[] args)
        {
            var sendEmailAddress = string.Empty;
            var sendEmailPassword = string.Empty;
            var recivedEmailAddress = string.Empty;

            var mailClient =
                new SmtpClient("smtp.189.cn", 25)
                {
                    EnableSsl = false,
                    Credentials = new NetworkCredential(sendEmailAddress, sendEmailPassword)
                };

            var mailMessage = new MailMessage(new MailAddress(sendEmailAddress, "测试发件", Encoding.UTF8),
                new MailAddress("recivedEmailAddress", "Hello", Encoding.UTF8))
            {
                Subject = "测试邮件主题",
                SubjectEncoding = Encoding.UTF8,
                Body = "测试邮件内容",
                BodyEncoding = Encoding.UTF8,
                BodyTransferEncoding = TransferEncoding.Base64
            };

            var assembly = Assembly.GetAssembly(typeof(Program));
            foreach (var name in assembly.GetManifestResourceNames())
            {
                var stream = assembly.GetManifestResourceStream(name);
                mailMessage.Attachments.Add(new Attachment(stream, name));
            }

            var sw = Stopwatch.StartNew();
            mailClient.Send(mailMessage);

            sw.Stop();

            Console.WriteLine($"发送完毕，耗时：{sw.ElapsedMilliseconds}ms");
            Console.ReadKey();
        }
    }
}