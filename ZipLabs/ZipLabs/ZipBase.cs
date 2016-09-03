using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZipLabs
{
    public enum ZipType
    {
        Rar,
        Zip
    }

    public abstract class ZipBase
    {
        /// <summary>
        /// 存放测试源文件的目录
        /// </summary>
        public static string ZipSourceDirForTest => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestFiles");

        /// <summary>
        /// 解压缩的根目录
        /// </summary>
        public static string UnZipRootDir => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "UnZipDir");

        /// <summary>
        /// 压缩库的名称
        /// </summary>
        public abstract string LibName { get; }

        /// <summary>
        /// 解压文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        public abstract bool TestUnZip(string filePath);

        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="fileOrDirectoryPath"></param>
        /// <returns></returns>
        public abstract bool TestZip(string fileOrDirectoryPath);
    }
}
