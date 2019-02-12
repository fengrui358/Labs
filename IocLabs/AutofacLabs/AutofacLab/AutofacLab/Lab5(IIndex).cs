using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Autofac.Features.Indexed;

namespace AutofacLab
{
    public class Lab5
    {
        private readonly IContainer _container;

        public Lab5()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<DerivedB>().Keyed<B>("first");
            builder.RegisterType<AnotherDerivedB>().Keyed<B>("second");
            builder.RegisterType<A>();
            _container = builder.Build();
        }

        public void WriteString()
        {
            Console.WriteLine(_container.Resolve<A>().WriteString());
        }
    }

    public class A
    {
        private IIndex<string, B> _b;

        public A(IIndex<string, B> b)
        {
            _b = b;
        }

        public string WriteString()
        {
            var derivedB = _b["first"];
            B anotherDerivedB = null;

            if (_b.TryGetValue("second", out var b))
            {
                anotherDerivedB = b;
            }
            return $"{derivedB.WriteString()}{Environment.NewLine}{anotherDerivedB?.WriteString()}";
        }
    }

    public class B
    {
        public virtual string WriteString()
        {
            return nameof(B);
        }
    }

    public class DerivedB : B
    {
        public override string WriteString()
        {
            return nameof(DerivedB);
        }
    }

    public class AnotherDerivedB : B
    {
        public override string WriteString()
        {
            return nameof(AnotherDerivedB);
        }
    }
}