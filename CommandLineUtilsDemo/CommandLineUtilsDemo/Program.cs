using System;
using McMaster.Extensions.CommandLineUtils;

namespace CommandLineUtilsDemo
{
    /// <summary>
    /// 项目地址：https://github.com/natemcmaster/CommandLineUtils
    /// https://natemcmaster.github.io/CommandLineUtils/
    /// </summary>
    [HelpOption]
    public class Program
    {
        public static int Main(string[] args)
            => CommandLineApplication.Execute<Program>(args);

        [Option(Description = "The subject")]
        public string Subject { get; }

        private void OnExecute()
        {
            var subject = Subject ?? "world";
            Console.WriteLine($"Hello {subject}!");
        }
    }
}
