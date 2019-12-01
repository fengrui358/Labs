using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;

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

            var lab7 = new Lab7();
            lab7.WriteString();
            Console.WriteLine();

            var lab8 = new Lab8();
            var i1 = lab8.Container.Resolve<IEnumerable<ITestClass1>>();

            foreach (var testClass1 in i1)
            {
                var x = testClass1.GetHashCode();
            }

            var i2 = lab8.Container.Resolve<ITestClass2>();
            var i3 = lab8.Container.Resolve<TestClass>();
            var i4 = lab8.Container.Resolve<TestClass2>();

            var h0 = lab8.TestClass.GetHashCode();
            var h1 = i1.GetHashCode();
            var h2 = i2.GetHashCode();
            var h3 = i3.GetHashCode();

            //Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
}