using System;
using System.ComponentModel.DataAnnotations;
using McMaster.Extensions.CommandLineUtils;

namespace InteractiveCommandLineDemo
{
    /// <summary>
    /// 带交互的命令工具的Demo
    /// https://github.com/natemcmaster/CommandLineUtils/blob/master/samples/Subcommands/Program.cs
    /// </summary>
    [Command(ThrowOnUnexpectedArgument = false)]
    class Program
    {
        public static int Main(string[] args) => CommandLineApplication.Execute<Program>(args);

        [Argument(0), Range(1, 3)] public int? Option { get; set; }

        public string[] RemainingArgs { get; set; }

        private int OnExecute()
        {
            const string prompt = @"Which example would you like to run?
1 - Fake Git
2 - Fake Docker
3 - Fake npm
> ";
            if (!Option.HasValue)
            {
                Option = Prompt.GetInt(prompt);
            }

            if (RemainingArgs == null || RemainingArgs.Length == 0)
            {
                var args = Prompt.GetString("Enter some arguments >");
                RemainingArgs = args.Split(' ');
            }

            switch (Option)
            {
                //case 1:
                //    return CommandLineApplication.Execute<Git>(RemainingArgs);
                //case 2:
                //    return CommandLineApplication.Execute<Docker>(RemainingArgs);
                //case 3:
                //    return Npm.Main(RemainingArgs);
                default:
                    Console.Error.WriteLine("Unknown option");
                    return 1;
            }
        }
    }
}
