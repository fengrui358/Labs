using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace ZipLabs
{
    class Program
    {
        static void Main(string[] args)
        {
            ClearLastTestDir();

            var zipFiles =
                new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestFiles")).GetFiles();

            var zips = new List<ZipBase> {new SharpCompress()};

            foreach (var zipBase in zips)
            {
                foreach (var fileInfo in zipFiles)
                {
                    var result = zipBase.TestUnZip(fileInfo.FullName) ? "成功" : "失败";                    
                    Console.WriteLine($"【{zipBase.LibName}】解压【{fileInfo.Name}】{result}");
                }
            }

            Console.ReadKey();
        }

        private static void ClearLastTestDir()
        {
            try
            {
                if (Directory.Exists(ZipBase.UnZipRootDir))
                {
                    Directory.Delete(ZipBase.UnZipRootDir, true);
                }

                Directory.CreateDirectory(ZipBase.UnZipRootDir);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }
    }
}
