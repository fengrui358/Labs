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
        {
            //var app = new CommandLineApplication {ExtendedHelpText = "这是一个测试程序"};
            //return app.Execute(args);

            return CommandLineApplication.Execute<Options>(args);
        }
            

        [Option(Description = "The subject")]
        public string Subject { get; }


    }
}
