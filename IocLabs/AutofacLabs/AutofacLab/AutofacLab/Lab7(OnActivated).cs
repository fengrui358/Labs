using System;
using Autofac;

namespace AutofacLab
{
    public class Lab7
    {
        private readonly IContainer _container;

        public Lab7()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ClassLab7>().AsSelf().OnActivated(s => s.Instance.Activated())
                .OnActivating(s => s.Instance.Activating()).OnPreparing(s => Console.WriteLine("Preparing"));
            _container = builder.Build();
        }

        public void WriteString()
        {
            var lab7 = _container.Resolve<ClassLab7>();
            GC.KeepAlive(lab7);
        }
    }

    public class ClassLab7
    {
        public ClassLab7()
        {
            Console.WriteLine(nameof(ClassLab7) + "Constructor");
        }

        public void Activating()
        {
            Console.WriteLine(nameof(Activating));
        }

        public void Activated()
        {
            Console.WriteLine(nameof(Activated));
        }
    }
}