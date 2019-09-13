using System;
using System.Diagnostics;
using System.IO;
using SharpCompress.Archives.Rar;
using SharpCompress.Archives.Zip;
using SharpCompress.Common;
using SharpCompress.Readers;
using SharpCompress.Readers.Rar;
using SharpCompress.Readers.Zip;
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
                            reader = ZipReader.Open(stream, new ReaderOptions {Password = "123"});
                        }
                        else if (RarArchive.IsRarFile(filePath))
                        {
                            reader = RarReader.Open(stream, new ReaderOptions {Password = "123"});
                        }
                    }
                    else
                    {
                        reader = ReaderFactory.Open(stream);
                    }

                    var subGuidDir =
                                new DirectoryInfo(Path.Combine(ZipBase.UnZipRootDir, Guid.NewGuid().ToString("N")));
                    subGuidDir.Create();
                    
                    if (reader != null)
                    {
                        while (reader.MoveToNextEntry())
                        {
                            if (!reader.Entry.IsDirectory)
                            {
                                reader.WriteEntryToDirectory(subGuidDir.FullName,
                                    new ExtractionOptions {ExtractFullPath = true, Overwrite = true});
                            }
                        }

                        return VerifyManager.Verify(subGuidDir.FullName);
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
