using System;
using System.Threading;
using Newtonsoft.Json;

namespace TimeZoneLab
{
    class Program
    {
        private static DateTime _lastSecondTime = DateTime.Now;
        private static long? _lastUtcOffsetTicks;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var time = new Timer(Callback);

            time.Change(0, 1000);

            Console.ReadLine();
        }

        private static void Callback(object? state)
        {
            var t = new TimeZoneTest
            {
                TestTime = DateTime.Now
            };

            if (_lastUtcOffsetTicks == null)
            {
                Console.WriteLine($"now time zone: {TimeZoneInfo.Local.BaseUtcOffset.Ticks}");
            }
            else
            {
                Console.WriteLine($"now time zone: {TimeZoneInfo.Local.BaseUtcOffset.Ticks}   last time zone：{_lastUtcOffsetTicks}");
                if (TimeZoneInfo.Local.BaseUtcOffset.Ticks != _lastUtcOffsetTicks)
                {
                    Console.WriteLine("time zone changed");
                }
            }
            
            Console.WriteLine($"now: {t.TestTime}   last second: {_lastSecondTime}");
            Console.WriteLine($"now utc time: {TimeZoneInfo.ConvertTimeToUtc(t.TestTime)}   last utc time: {TimeZoneInfo.ConvertTimeToUtc(_lastSecondTime)}");
            Console.WriteLine($"now json: {JsonConvert.SerializeObject(t)}");
            Console.WriteLine();

            _lastSecondTime = t.TestTime;
            _lastUtcOffsetTicks = TimeZoneInfo.Local.BaseUtcOffset.Ticks;
        }
    }
}
