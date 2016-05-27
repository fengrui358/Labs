using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dapper.FastCrud;
using MySql.Data.MySqlClient;

namespace UniqueLab
{
    class Program
    {
        private const int Total = 100000;
        private static string dbName;
        private static Guid beacon = Guid.NewGuid();

        static void Main(string[] args)
        {
            OrmConfiguration.DefaultDialect = SqlDialect.MySql;

            var random = new Random().Next(1, 10000);
            dbName = string.Format("testdb{0}", random);

            using (
                var con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnString"].ConnectionString)
                )
            {
                //创建数据库
                var createDbSql = string.Format("CREATE DATABASE IF NOT EXISTS `{0}`;", dbName);
                con.Execute(createDbSql);
                con.Open();

                con.ChangeDatabase(dbName);

                //创建表
                var sql = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "user.sql.txt"));

                con.Execute(sql);
                con.Close();
            }


            using (
                var con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnString"].ConnectionString)
                )
            {
                con.Open();

                con.ChangeDatabase(dbName);

                con.Close();
            }

            Console.WriteLine("准备插入数据");

            var becaonFlag = new Random(DateTime.Now.Millisecond).Next(1, Total);
            var tempUsers = new User[Total];
            for (int i = 0; i < Total; i++)
            {
                if (becaonFlag == i)
                {
                    var user = new User {OpenId = beacon.ToString()};
                    tempUsers[i] = user;
                }
                else
                {
                    tempUsers[i] = new User();
                }
            }

            BulkInsertAsync(tempUsers);
            
            var repeatUser = new User { OpenId = beacon.ToString() };

            Console.WriteLine("开始测试抛异常");

            var sw = Stopwatch.StartNew();

            try
            {
                using (
                    var con =
                        new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnString"].ConnectionString)
                    )
                {
                    con.Open();
                    con.ChangeDatabase(dbName);

                    try
                    {
                        con.Insert(repeatUser);
                    }
                    finally
                    {
                        con.Close();
                    }                    
                }                
            }
            catch (Exception ex)
            {
                sw.Stop();

                Console.WriteLine(ex);
                Console.WriteLine($"抛异常耗时：{sw.ElapsedMilliseconds}");
            }

            sw.Restart();

            using (
                var con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnString"].ConnectionString)
                )
            {
                con.Open();
                con.ChangeDatabase(dbName);

                var count = con.Count<User>(
                    statement =>
                        statement.Where($"{nameof(User.OpenId):C}=@OpenIdParm")
                            .WithParameters(new { OpenIdParm = repeatUser.OpenId }));

                sw.Stop();
                if (count > 0)
                {
                    Console.WriteLine($"发现重复数据，耗时：{sw.ElapsedMilliseconds}");
                }

                con.Close();
            }

            Console.WriteLine("测试完毕，点击任意键删除测试数据库");

            Console.ReadKey();

            using (
                var con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnString"].ConnectionString)
                )
            {
                //删除数据库
                con.Execute(string.Format("DROP DATABASE IF EXISTS `{0}`;", dbName));
            }
        }

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="users"></param>
        private async static void BulkInsertAsync(IEnumerable<User> users)
        {
            using (
                var con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnString"].ConnectionString)
                )
            {                
                await con.OpenAsync();
                await con.ChangeDataBaseAsync(dbName);

                var transaction = await con.BeginTransactionAsync();

                foreach (var user in users)
                {
                    con.Insert(user, builder => builder.AttachToTransaction(transaction));
                }
               
                transaction.Commit();

                con.Close();
            }
        }
    }
}
