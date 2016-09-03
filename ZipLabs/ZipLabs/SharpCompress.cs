using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCompress.Archive.Rar;
using SharpCompress.Archive.Zip;
using SharpCompress.Reader;
using SharpCompress.Reader.Rar;
using SharpCompress.Reader.Zip;
using ZipLabs.VerifyStrategies;

namespace ZipLabs
{
    /// <summary>
    /// https://github.com/adamhathcock/sharpcompress
    /// </summary>
    public class SharpCompress : ZipBase
    {
        public override string LibName => nameof(SharpCompress);

        public override bool TestUnZip(string filePath)
        {
            try
            {
                using (var stream = File.OpenRead(filePath))
                {
                    IReader reader = null;

                    if (IsEncryptFile(filePath))
                    {
                        if (ZipArchive.IsZipFile(filePath))
                        {
                            reader = ZipReader.Open(stream, "123");
                        }
                        else if (RarArchive.IsRarFile(filePath))
                        {
                            reader = RarReader.Open(stream, "123");
                        }
                    }
                    else
                    {
                        reader = ReaderFactory.Open(stream);
                    }

                    if (reader != null)
                    {
                        while (reader.MoveToNextEntry())
                        {
                            if (!reader.Entry.IsDirectory)
                            {
                                var subGuidDir =
                                new DirectoryInfo(Path.Combine(ZipBase.UnZipRootDir, Guid.NewGuid().ToString("N")));
                                subGuidDir.Create();

                                reader.WriteEntryToDirectory(subGuidDir.FullName);
                                return VerifyManager.Verify(subGuidDir.FullName);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return false;
        }

        public override bool TestZip(string fileOrDirectoryPath)
        {
            throw new NotImplementedException();
        }

        private bool IsEncryptFile(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                var fileInfo = new FileInfo(filePath);
                if (fileInfo.Exists)
                {
                    return fileInfo.Name.Contains("密码");
                }
            }
       
            return false;
        }
    }
}
