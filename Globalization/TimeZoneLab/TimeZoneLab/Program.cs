using System;
using System.Threading;
using Newtonsoft.Json;

namespace TimeZoneLab
{
    class Program
    {
        private static readonly DateTime StartTime = DateTime.Now;
        private static bool _isDaylightSavingTime = TimeZoneInfo.Local.IsDaylightSavingTime(DateTime.Now);

        static void Main(string[] args)
        {
            var t = "2020-09-12 14:22:45";
            //测试结论1：接受一个不带时区的字符串，用DateTime.Parse转换会默认使用当前时区
            var t1 = DateTime.Parse(t);
            var t2 = TimeZoneInfo.ConvertTimeToUtc(t1);

            //时区切换，以墨西哥城时间为例:  http://www.timeofdate.com/city/Mexico/Mexico%20City/timezone/change
            //设置操作系统时间，关闭“自动设置时间”和“自动设置时区”，手动调整时区到(UTC-06:00) 瓜达拉哈拉，墨西哥城，蒙特雷，手动设置时间到1:59，过1分钟后进入夏令时，时间转变为 3:00
            Console.WriteLine($"now is day light saving time?  {_isDaylightSavingTime}");
            Console.WriteLine();

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

            if (TimeZoneInfo.Local.IsDaylightSavingTime(t.TestTime) != _isDaylightSavingTime)
            {
                Console.WriteLine("time zone changed");
                _isDaylightSavingTime = TimeZoneInfo.Local.IsDaylightSavingTime(t.TestTime);
                Console.WriteLine();
            }

            Console.WriteLine($"now: {t.TestTime}   start second: {StartTime}");
            Console.WriteLine($"now utc time: {TimeZoneInfo.ConvertTimeToUtc(t.TestTime)}   start utc time: {TimeZoneInfo.ConvertTimeToUtc(StartTime)}");
            Console.WriteLine($"now json: {JsonConvert.SerializeObject(t)}");
            Console.WriteLine();
        }
    }
}
