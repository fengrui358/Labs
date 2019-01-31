using System;
using System.Collections.Generic;
using Autofac;
using Autofac.Core;

namespace AutofacLab
{
    public class Lab1
    {
        private readonly IContainer _container;

        public Lab1()
        {
            var builder = new ContainerBuilder();
            builder.Register<TodayWriter>((s, t) => new TodayWriter(s.Resolve<IOutput>(), t.Named<string>("test")))
                .As<IDateWriter>();

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
            using (var scope = _container.BeginLifetimeScope())
            {
                var writer = scope.Resolve<IDateWriter>(new List<Parameter> {new NamedParameter("test", "free")});
                writer.WriteDate();
            }

            //var writer = _container.Resolve<IDateWriter>();
            //writer.WriteDate();
        }
    }

    // This interface helps decouple the concept of
    // "writing output" from the Console class. We
    // don't really "care" how the Write operation
    // happens, just that we can write.
    public interface IOutput
    {
        void Write(string content);
    }

    // This implementation of the IOutput interface
    // is actually how we write to the Console. Technically
    // we could also implement IOutput to write to Debug
    // or Trace... or anywhere else.
    public class ConsoleOutput : IOutput
    {
        public void Write(string content)
        {
            Console.WriteLine(content);
        }
    }

    // This interface decouples the notion of writing
    // a date from the actual mechanism that performs
    // the writing. Like with IOutput, the process
    // is abstracted behind an interface.
    public interface IDateWriter
    {
        void WriteDate();
    }

    // This TodayWriter is where it all comes together.
    // Notice it takes a constructor parameter of type
    // IOutput - that lets the writer write to anywhere
    // based on the implementation. Further, it implements
    // WriteDate such that today's date is written out;
    // you could have one that writes in a different format
    // or a different date.
    public class TodayWriter : IDateWriter
    {
        private readonly IOutput _output;
        private readonly string _name;

        public TodayWriter(IOutput output, string name = null)
        {
            _output = output;
            _name = name;
        }

        public void WriteDate()
        {
            _output.Write($"{_name} {DateTime.Today.ToShortDateString()}");
        }
    }
}