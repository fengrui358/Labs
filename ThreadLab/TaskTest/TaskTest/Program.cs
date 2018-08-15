using System;
using System.Threading.Tasks;
using Nito.AsyncEx;

namespace TaskTest
{
    class Program
    {
        static int Main(string[] args)
        {
            try
            {
                var result = AsyncContext.Run(() => MainAsync(args));
                Console.Read();

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return -1;
            }
        }

        static async Task<int> MainAsync(string[] args)
        {
            await TaskAttachedToParentTest.ParentTask();
            return 1;
        }
    }
}
