using System;
using System.IO;
using System.Threading.Tasks;
using SQLite;

namespace SQLiteNetLab
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cadlocaldata.db");

            var cn = new SQLiteAsyncConnection(path);

            var all = await cn.Table<ReadUnread>().ToListAsync();

            var allByUser =
                await cn.Table<ReadUnread>().Where(s => s.UserId == "1205041586367389698").ToListAsync();

            var result = await cn.Table<ReadUnread>().Where(s => s.UserId == "1205041586367389698" && s.Key == "1204674403909099522").ToListAsync();

            await cn.CloseAsync();
            Console.ReadKey();
        }
    }
}
