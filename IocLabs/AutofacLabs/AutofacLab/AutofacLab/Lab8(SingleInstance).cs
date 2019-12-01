using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Autofac.Features.Variance;

namespace AutofacLab
{
    public class Lab8
    {
        public IContainer Container { get; }

        public TestClass TestClass { get; }

        private ContainerBuilder _builder;

        public Lab8()
        {
            _builder = new ContainerBuilder();
            TestClass = new TestClass();

            _builder.RegisterTypes(typeof(TestClass), typeof(TestClass2)).AsImplementedInterfaces().AsSelf().SingleInstance();

            Container = _builder.Build();
        }
    }

    public class TestClass : ITestClass1, ITestClass2
    {

    }

    public interface ITestClass1
    {

    }

    public interface ITestClass2
    {

    }

    public class TestClass2 : ITestClass1
    {

    }
}
