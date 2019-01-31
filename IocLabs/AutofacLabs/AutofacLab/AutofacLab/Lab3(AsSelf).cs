using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Autofac.Core;

namespace AutofacLab
{
    /// <summary>
    /// AsSelf方法暴露接口服务的同时暴露自身
    /// </summary>
    public class Lab3
    {
        private readonly IContainer _container;

        public Lab3()
        {
            var builder = new ContainerBuilder();
            builder.Register<TodayWriter>((s, t) => new TodayWriter(s.Resolve<IOutput>(), t.Named<string>("test")))
                .As<IDateWriter>().AsSelf().InstancePerLifetimeScope();

            builder.RegisterType<ConsoleOutput>().As<IOutput>();
            //builder.RegisterType<TodayWriter>().As<IDateWriter>();
            _container = builder.Build();
        }

        // The WriteDate method is where we'll make use
        // of our dependency injection. We'll define that
        // in a bit.
        public void WriteDate()
        {
            // Create the scope, resolve your IDateWriter,
            // use it, then dispose of the scope.
            var writer = _container.Resolve<IDateWriter>(new List<Parameter> {new NamedParameter("test", "free")});
            writer.WriteDate();

            //var writer = _container.Resolve<IDateWriter>();
            //writer.WriteDate();
        }

        public void WriteSelfDate()
        {
            // Create the scope, resolve your IDateWriter,
            // use it, then dispose of the scope.
            var writer = _container.Resolve<TodayWriter>(new List<Parameter> {new NamedParameter("test", "free")});
            writer.WriteDate();

            //var writer = _container.Resolve<IDateWriter>();
            //writer.WriteDate();
        }
    }
}