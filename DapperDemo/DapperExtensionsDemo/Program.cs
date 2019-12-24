using System;
using System.Data.SQLite;
using System.IO;
using System.Threading.Tasks;
using DapperExtensions;

namespace DapperExtensionsDemo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cadlocaldata.db");

            using (var cn = new SQLiteConnection($"data source={path}"))
            {
                var all = await cn.GetListAsync<ReadUnread>();

                var allByUser =
                    await cn.GetListAsync<ReadUnread>(Predicates.Field<ReadUnread>(s => s.UserId, Operator.Eq,
                        "1205041586367389698"));

                var predicates = Predicates.Group(GroupOperator.And,
                    Predicates.Field<ReadUnread>(s => s.UserId, Operator.Eq, "1205041586367389698"),
                    Predicates.Field<ReadUnread>(s => s.Key, Operator.Eq, "1204674403909099522"));

                var result = await cn.GetListAsync<ReadUnread>(predicates);
            }

            Console.ReadKey();
        }
    }
}