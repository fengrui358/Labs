using System;
using ProbingPrivatepathLib;

namespace ProbingPrivatepathLab
{
    class Program
    {
        static void Main(string[] args)
        {
            var hello = new HelloWorld();
            Console.WriteLine(hello.SayHello());

            Console.ReadKey();
        }
    }
}
