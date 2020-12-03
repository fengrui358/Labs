using System;
using System.Globalization;
using System.Text.Json;
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
            Console.WriteLine("可使用有夏令时的时区测试本程序，(UTC-06:00) 瓜达拉哈拉，墨西哥城，蒙特雷");
            Console.WriteLine($"当前时区为：{TimeZoneInfo.Local.DisplayName}");
            Console.WriteLine();

            var t = "2020-09-12 14:22:45";
            //测试结论1：接受一个不带时区的字符串，用DateTime.Parse转换会默认使用当前时区
            var t1 = DateTime.Parse(t);
            var t2 = TimeZoneInfo.ConvertTimeToUtc(t1);

            //时区切换，以墨西哥城时间为例:  http://www.timeofdate.com/city/Mexico/Mexico%20City/timezone/change
            //设置操作系统时间，关闭“自动设置时间”和“自动设置时区”，手动调整时区到(UTC-06:00) 瓜达拉哈拉，墨西哥城，蒙特雷，手动设置时间到1:59，过1分钟后进入夏令时，时间转变为 3:00
            Console.WriteLine($"now is day light saving time?  {_isDaylightSavingTime}");
            Console.WriteLine();

            ManualVerify();

            Console.WriteLine("循环测试开始：");
            Console.WriteLine();
            var time = new Timer(Callback);

            time.Change(0, 1000);

            Console.ReadLine();
        }

        private static DateTime T_1_59_59;
        private static DateTime T_1_0_0;

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

            //切换2020-10-25 01：59：59会记录这两个时间进行比较
            if (t.TestTime.Second == 59)
            {
                T_1_59_59 = t.TestTime;
            }

            if (t.TestTime.Second == 0)
            {
                T_1_0_0 = t.TestTime;
                var diff = T_1_0_0.Subtract(T_1_59_59);
                var diff2 = new DateTimeOffset(T_1_0_0) - new DateTimeOffset(T_1_59_59); //构造DateTimeOffset会判断是否位UTC时间，如果不是会使用本地时区来生成偏移时区
            }

            Console.WriteLine($"now: {t.TestTime}   start second: {StartTime}");
            Console.WriteLine($"now utc time: {TimeZoneInfo.ConvertTimeToUtc(t.TestTime)}   start utc time: {TimeZoneInfo.ConvertTimeToUtc(StartTime)}");

            //测试结论3：各种不同的时间格式
            /*
            "\"\\/Date(1335205592410)\\/\""         .NET JavaScriptSerializer
            "\"\\/Date(1335205592410-0500)\\/\"".NET DataContractJsonSerializer
            "2012-04-23T18:25:43.511Z"              JavaScript built-in JSON object
            "2012-04-21T18:25:43-05:00"             ISO 8601
            */

            //Console.WriteLine($"now json: {JsonConvert.SerializeObject(t)}"); //json 序列化默认时间格式IsoDateFormat{"TestTime":"2020-12-02T23:37:57.5804267+08:00"}

            var dateTimeSerializerSettings = new JsonSerializerSettings
            {
                DateTimeZoneHandling = DateTimeZoneHandling.Utc
            };
            Console.WriteLine($"now json: {JsonConvert.SerializeObject(t, dateTimeSerializerSettings)}");//json 使用Utc标准时间格式{"TestTime":"2020-12-02T15:40:09.0625343Z"}

            //var dateTimeSerializerSettings = new JsonSerializerSettings
            //{
            //    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
            //};
            //Console.WriteLine($"now json: {JsonConvert.SerializeObject(t, dateTimeSerializerSettings)}");//json 使用MicrosoftDateFormat{"TestTime":"\/Date(1606923766013+0800)\/"}
            Console.WriteLine();
        }

        /// <summary>
        /// 手动验证
        /// </summary>
        private static void ManualVerify()
        {
            //UTC 时间进入系统后会自动转换为带时区的时间，主要是DateTime的Kind值不一样
            var t = new DateTime(2020, 4, 5, 2, 0,0, DateTimeKind.Utc);
            var t2 = new DateTime(2020, 4, 5, 2, 0, 0, DateTimeKind.Local);
            var t3 = new DateTime(2020, 4, 5, 2, 0, 0);

            //消失的一个消失，推论 墨西哥城不会存在2020年4月5日2点到2020年4月5日2点59分的任何时间值。
            //不能生成时间成功，这个时间会成为一个无效时间，在带时区的时间显示中，你会看见它无法显示
            var 不正常时间1 = new DateTime(2020, 4, 5, 2, 0, 0, DateTimeKind.Local);
            var 不正常时间2 = new DateTime(2020, 4, 5, 2, 59, 59, DateTimeKind.Local);
            Console.WriteLine(不正常时间1);
            Console.WriteLine(不正常时间2);
            var 正常时间3 = new DateTime(2020, 4, 5, 1, 59, 59, DateTimeKind.Local);
            var 正常时间4 = new DateTime(2020, 4, 5, 3, 0, 0, DateTimeKind.Local);

            //冬令时
            var 正常时间5 = new DateTime(2020,10,25,0, 59, 59, DateTimeKind.Local);//用代码new出来的时间得到的实际是夏令时结束的1点
            Console.WriteLine(TimeZoneInfo.ConvertTimeToUtc(正常时间5));
            var 正常时间6 = 正常时间5.AddSeconds(1);
            Console.WriteLine(TimeZoneInfo.ConvertTimeToUtc(正常时间6));
            Console.WriteLine(TimeZoneInfo.ConvertTimeToUtc((new DateTimeOffset(正常时间5)).AddSeconds(1).LocalDateTime));

            //如何表示夏令时的2020年10月25日1点内的时间
            //用代码new出来的是冬令时的1点

            //展示他们的UTC时间
            //对无效时间进行UTC转换时会得到一个报错
            //System.ArgumentException:“The supplied DateTime represents an invalid time.  For example, when the clock is adjusted forward, any time in the period that is skipped is invalid. ”
            //Console.WriteLine(TimeZoneInfo.ConvertTimeToUtc(不正常时间1));
            //Console.WriteLine(TimeZoneInfo.ConvertTimeToUtc(不正常时间2));
            Console.WriteLine(TimeZoneInfo.ConvertTimeToUtc(正常时间3));
            Console.WriteLine(TimeZoneInfo.ConvertTimeToUtc(正常时间4));

            //与不正常时间进行运算 时间差值没有考虑时区的不一样，所以得到的值不正确
            var s = TimeZoneInfo.Local.IsInvalidTime(不正常时间2); //判断是否为不正常时间，true
            var s2 = TimeZoneInfo.Local.IsInvalidTime(正常时间3); //判断是否为不正常时间，false
            var 不正常时差1 = 不正常时间2 - 不正常时间1;
            var 不正常时差2 = 正常时间4 - 不正常时间2;
            var 不正常时差3 = new DateTimeOffset(正常时间4) - new DateTimeOffset(不正常时间2); //不会报错

            //正常时间运算
            var 正常时差 = 正常时间4 - 正常时间3; //结果为1小时1分，时间差值没有考虑时区的不一样，所以得到的值不正确
            var 正常时差2 = new DateTimeOffset(正常时间4) - new DateTimeOffset(正常时间3); //相差1秒，结果正确
            var 正常时差3 = TimeZoneInfo.ConvertTimeToUtc(正常时间4) - TimeZoneInfo.ConvertTimeToUtc(正常时间3); //相差1秒，结果正确
        }
    }
}
