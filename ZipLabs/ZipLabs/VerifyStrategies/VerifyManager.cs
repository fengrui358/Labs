using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZipLabs.VerifyStrategies
{
    public class VerifyManager
    {
        public static bool Verify(string newUnZipDir)
        {
            var result = false;

            try
            {
                result = Verify_TestZip(newUnZipDir);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            
            return result;
        }

        private static bool Verify_TestZip(string newUnZipDir)
        {
            var dir = new DirectoryInfo(newUnZipDir);
            var subDirs = dir.GetDirectories();
            FileInfo testFile = null;

            var subDir = subDirs.FirstOrDefault(s => s.Name == "测试目录");
            if (subDir != null)
            {
                testFile = subDir.GetFiles("测试.txt").First();
            }
            else
            {
                testFile = dir.GetFiles("测试.txt").First();
            }

            using (var readStream = testFile.OpenRead())
            {
                var reader = new StreamReader(readStream, Encoding.Default);
                var content = reader.ReadToEnd();
                if (content == "测试")
                {
                    return true;
                }
            }

            return false;
        }

        private static bool Verify_TestZipDir(string newUnZipDir)
        {
            var dir = new DirectoryInfo(newUnZipDir);
            var testDir = dir.GetDirectories().First();
            if (testDir.Name == "测试")
            {
                return Verify_TestZip(testDir.FullName);
            }

            return false;
        }        
    }
}
