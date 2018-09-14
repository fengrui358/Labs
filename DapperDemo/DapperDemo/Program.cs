using System;
using System.Data.SQLite;
using System.IO;
using Dapper;

namespace DapperDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "sqlite.db");

            var copyPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "sqlite(copy).db");

            File.Copy(path, copyPath, true);

            using (var cn = new SQLiteConnection($"data source={copyPath}"))
            {
                var createTablesSql =
                    File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CreateTables.sql"));

                cn.Execute(createTablesSql);

                var ss = cn.GetListAsync<User>().GetAwaiter().GetResult();

                var id = cn.Insert(new User {UserName = "free", Address = "chengdu"});
            }

            Console.ReadKey();
        }
    }
}
