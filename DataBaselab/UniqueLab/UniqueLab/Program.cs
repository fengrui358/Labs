using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MySql.Data.MySqlClient;

namespace UniqueLab
{
    class Program
    {
        private const int Total = 100000;

        static void Main(string[] args)
        {            
            var random = new Random().Next(1,10000);
            var dbName = string.Format("testdb{0}", random);

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
    }
}
