using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace ConfigurationDemo
{
    /// <summary>
    /// http://video.jessetalk.cn/course/4/task/10/show
    /// 需引用：Microsoft.Extensions.Configuration.CommandLine
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var setting = new Dictionary<string, string>
            {
                {"name", "free memery"},
                {"age", "67"}
            };

            var builder = new ConfigurationBuilder().AddInMemoryCollection(setting).AddCommandLine(args);

            var configuration = builder.Build();

            Console.WriteLine($"name:{configuration["name"]}");
            Console.WriteLine($"age:{configuration["age"]}");

            Console.ReadLine();
        }
    }
}