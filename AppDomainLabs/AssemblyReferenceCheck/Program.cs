using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyReferenceCheck
{
    class Program
    {
        static void Main(string[] args)
        {
            var allFilesTobeCheck = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll",
                SearchOption.AllDirectories);

            foreach (var s in allFilesTobeCheck)
            {
                ReferenceManager.CheckFile(s);
            }

            var result = ReferenceManager.GetConflicts();
            var saveResult = new StringBuilder();
            foreach (var s in result)
            {
                Console.WriteLine(s);
                saveResult.AppendLine(s);
            }

            if (saveResult.Length > 0)
            {
                File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                        $"检测结果{DateTime.Now.ToString("yyyyMMddHHmmss")}.txt"), saveResult.ToString());
            }

            Console.WriteLine("探测完毕");
            Console.ReadKey();
        }
    }
}
