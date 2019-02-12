using System;

namespace AutofacLab
{
    class Program
    {
        static void Main()
        {
            var lab1 = new Lab1();
            lab1.WriteDate();
            lab1.WriteDate();
            Console.WriteLine();

            var lab2 = new Lab2();
            lab2.WriteString();
            Console.WriteLine();

            var lab3 = new Lab3();
            lab3.WriteDate();
            lab3.WriteSelfDate();
            Console.WriteLine();

            //var lab4 = new Lab4();
            //lab4.WriteString();

            var lab5 = new Lab5();
            lab5.WriteString();
            Console.WriteLine();

            var lab6 = new Lab6();
            lab6.WriteString();
            Console.WriteLine();

            //Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
}