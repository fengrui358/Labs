using System;
using Autofac;
using Autofac.Core;

namespace AutofacLab
{
    public class Lab4
    {
        private readonly IContainer _container;

        public Lab4()
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyModules(typeof(Lab4).Assembly);

            _container = builder.Build();
        }

        public void WriteString()
        {
            Console.WriteLine(_container.Resolve<ModuleA>().Get());
            Console.WriteLine(_container.Resolve<ModuleA>().Get());
            Console.WriteLine(_container.Resolve<ModuleB>().Get());
        }
    }

    public class ModuleA : Module
    {
        public ModuleA ()
        {
            
        }

        public string Get()
        {
            return nameof(ModuleA);
        }
    }

    public class ModuleB : Module
    {
        public ModuleB()
        {

        }

        public string Get()
        {
            return nameof(ModuleB);
        }
    }
}
