using System;
using System.ComponentModel.DataAnnotations;
using McMaster.Extensions.CommandLineUtils;

namespace CommandLineUtilsDemo
{
    [Command(Name = "done", Description = "Keep track on things you've done", ExtendedHelpText = @"测试
                                        
                                            ds大苏打")]
    public class Options
    {
        [Option(CommandOptionType.MultipleValue, LongName = "add", ShortName = "a", Description = "加法运算", SymbolName = "SymbolName",
            ValueName = "3 + 5")]
        public int[] Add { get; set; }

        [Option(Description = "输入打印信息")]
        [Required]
        public string Print { get; set; }

        [FileExists]
        [Option]
        public string[] Files { get; set; }

        private void OnExecute()
        {
            if (Add != null)
            {
                var sum = 0;
                foreach (var i in Add)
                {
                    sum += i;
                }

                Console.WriteLine(sum);
            }

            if (Print != null)
            {
                Console.WriteLine($"Print {Print}");
            }


            if (Files != null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("打印文件：");
                foreach (var file in Files)
                {
                    Console.WriteLine(file);
                }

                Console.ResetColor();
            }

            //Console.WriteLine($"Hello {subject}!");
            
        }
    }
}