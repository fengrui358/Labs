using System;
using Dapper.FastCrud;

namespace Dapper.NetCore.Demo
{
    /// <summary>
    /// https://github.com/MoonStorm/Dapper.FastCRUD
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            //设置默认参数
            OrmConfiguration.DefaultDialect = SqlDialect.SqLite;
            OrmConfiguration.DefaultSqlStatementOptions.CommandTimeout = TimeSpan.FromSeconds(30);
            



            Console.WriteLine("Hello World!");
        }
    }
}
