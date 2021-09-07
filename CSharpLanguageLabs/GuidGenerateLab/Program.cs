using System;

namespace GuidGenerateLab
{
    class Program
    {
        static void Main(string[] args)
        {
            var ticks = DateTimeOffset.UtcNow.Ticks; // 设置当前UTC时间
            var bits = Convert.ToString(ticks, 2);

            ticks = ticks << 48 >> 52;
            var bits2 = Convert.ToString(ticks, 2);
            //var time = new DateTime(ticks, DateTimeKind.Utc);

            var id = new SequentialGuidIDGenerator().Create(new SequentialGuidSettings());

            Console.WriteLine("Hello World!");
        }
    }
}
