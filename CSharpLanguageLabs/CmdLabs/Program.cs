using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmdLabs
{
    class Program
    {
        static void Main(string[] args)
        {
            //待打开的文件放入程序目录的Files目录下进行测试
            var files = Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files"));

            foreach (var file in files)
            {
                Console.WriteLine($"准备打开指定文件：{file}");
                OpenFileWithExplorer(file);
            }

            Console.ReadLine();
        }

        /// <summary>
        /// 利用Cmd让资源管理器打开指定文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        private static void OpenFileWithExplorer(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return;
            }

            var p = new Process
            {
                StartInfo =
                {
                    FileName = "cmd.exe",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardInput = true
                }
            };

            p.Start();

            p.StandardInput.WriteLine($"\"{filePath}\"");

            //打开文件无需等待响应
            //p.WaitForExit();
            p.Close();
        }
    }
}
