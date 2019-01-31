using System;
using Autofac;

namespace AutofacLab
{
    /// <summary>
    /// 注册泛型服务Generic
    /// </summary>
    public class Lab2
    {
        private readonly IContainer _container;

        public Lab2()
        {
            var builder = new ContainerBuilder();
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

            builder.RegisterType<RepositoryGuid>().As<IRepository<Guid>>().InstancePerLifetimeScope();

            _container = builder.Build();
        }

        public void WriteString()
        {
            Console.WriteLine($"开放泛型输出String，{_container.Resolve<IRepository<string>>().WriteData()}");
            Console.WriteLine($"开放泛型输出Int，{_container.Resolve<IRepository<int>>().WriteData()}");
            Console.WriteLine($"开放泛型输出Guid，{_container.Resolve<IRepository<Guid>>().WriteData()}");
        }
    }

    public interface IRepository<T>
    {
        T WriteData();
    }

    public class Repository<T> : IRepository<T>
    {
        public virtual T WriteData()
        {
            if (typeof(T).IsAssignableFrom(typeof(string)))
            {
                return (T) Convert.ChangeType("free", typeof(T));
            }
            else if (typeof(T).IsAssignableFrom(typeof(int)))
            {
                return (T) Convert.ChangeType(45, typeof(T));
            }

            return default(T);
        }
    }

    public class RepositoryGuid : Repository<Guid>
    {
        public override Guid WriteData()
        {
            return Guid.NewGuid();
        }
    }
}