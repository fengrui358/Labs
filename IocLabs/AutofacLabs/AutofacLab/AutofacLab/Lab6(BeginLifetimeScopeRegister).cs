using System;
using Autofac;

namespace AutofacLab
{
    public class Lab6
    {
        private readonly IContainer _container;

        public Lab6()
        {
            var builder = new ContainerBuilder();

            _container = builder.Build();
        }

        public void WriteString()
        {
            using (var scope = _container.BeginLifetimeScope(
                b => b.RegisterType<Lab6Class>().AsSelf()))
            {
                Console.WriteLine(scope.Resolve<Lab6Class>().Get());
            }

            Console.WriteLine(value: _container.ResolveOptional<Lab6Class>()?.Get());
        }
    }

    public class Lab6Class
    {
        public string Get()
        {
            return nameof(Lab6Class);
        }
    }
}