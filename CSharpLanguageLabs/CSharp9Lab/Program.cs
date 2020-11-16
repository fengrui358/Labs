using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using CSharp9Lab.Init;
using CSharp9Lab.Record;
using CSharp9Lab.PatternMatching;

/// <summary>
/// https://devblogs.microsoft.com/dotnet/c-9-0-on-the-record/
/// https://mp.weixin.qq.com/s?__biz=MzA5MzY2MzYwNQ==&mid=2247484537&idx=2&sn=ab02ba268c3d5c7da88648d0fa730cf3&chksm=905b3c63a72cb57507ab1658171923002eb3a26bc0472eda18a06f48e630faaab1fc5d54334b&mpshare=1&scene=1&srcid=1115MrNWaHSYcxsWsXshN1nl&sharer_sharetime=1605451857902&sharer_shareid=b31a30bc4e7b542ce39975aa65dedea9&key=f435df79f31f75ea136fa1eae760e33f575b9bcb3805fd63c2042e66ec168dd3d2abe7c167f22188359e5ba349bd085e7cad5b7c12231294b1d3cac6c0bc957ae89d2371959e1cdd96bcb4e7c078b1a2b6e239d99e599a377c3f3a4b0838641abb1f6b7e2b9e6eb7b9414bf6dc7b5c83c620b2b1b77a86bc3b2323b335eaa03e&ascene=1&uin=Nzg2NTI3NQ%3D%3D&devicetype=Windows+10+x64&version=6300002f&lang=zh_CN&exportkey=AxHJcfnCxpvPWbml4xlFYmY%3D&pass_ticket=uxQn1yawtbNYP5hTIohJWK9yKaCuQAna0hNZ5BV%2F9PnUT6vDndxxu03nPQbVM3ST&wx_header=0
/// https://www.cnblogs.com/tcjiaan/p/13947928.html
/// </summary>

#region Record 不可变引用类型

var p1 = new Person("Bill", "Wagner");
var p2 = new Person("Bill", "Wagner");
var p3 = new Person2("Bill", "Wagner");

//p1和p2相等
Console.WriteLine(p1);
Console.WriteLine($"p1 == p2  {p1 == p2}");

var t1 = new Teacher("Bill", "Wagner", "T");
var t2 = new Teacher("Bill", "Wagner", "T");
Console.WriteLine(t1);
Console.WriteLine($"p3 == t2  {p3 == t2}");
Console.WriteLine($"t1 == t2  {t1 == t2}");

//测试使用使用位置记录的Student

var s1 = new Student("Bill", "Wagner", 2);
var s2 = new Student("Bill", "Wagner", 2);
Console.WriteLine(s2);
Console.WriteLine($"Student Name {s1.GetName()}");
Console.WriteLine($"p1 == s2  {p1 == s2}");
Console.WriteLine($"s1 == s2  {s1 == s2}");

//使用with修改记录副本，修改副本记录后不相等
var s2_OldHashCode = s2.GetHashCode();
s2 = s2 with { First = "Jack" };
Console.WriteLine($"s1 == s2  {s1 == s2}");
Console.WriteLine($"s2 old hash code {s2_OldHashCode}, s2 new hash code {s2.GetHashCode()}");

#endregion

#region Init 初始化属性Setter

var now = new WeatherObservation
{
    RecordedAt = DateTime.Now,

    TemperatureInCelsius = 20,

    PressureInMillibars = 998.0m
};

now = now with { TemperatureInCelsius = 45 };
Console.WriteLine(now);

#endregion

#region Pattern Matching

var od = new Order
{
    ID = 11,
    Company = "大嘴狗贸易有限公司",
    ContactName = "陈大爷",
    Qty = 425.12f,
    UP = 1000.55M,
    Date = new(2020, 10, 27)
};

switch (od)
{
    case { Qty: > 1000f }:
    Console.WriteLine("发财了，发财了");
    break;
    case { Qty: > 500f }:
    Console.WriteLine("好家伙，年度大订单");
    break;
    case { Qty: > 100f }:
    Console.WriteLine("订单量不错");
    break;
}

switch (od)
{
    case Order { Qty: > 1000f }:
    Console.WriteLine("发财了，发财了");
    break;
    case Order { Qty: > 500f }:
    Console.WriteLine("好家伙，年度大订单");
    break;
    case Order { Qty: > 100f }:
    Console.WriteLine("订单量不错");
    break;
}

var message = od switch
{
    { Qty: > 1000f } => "发财了",
    { Qty: > 500f } => "年度大订单",
    { Qty: > 100f } => "订单量不错",
    _ => "未知"
};

Console.WriteLine(message);

#endregion

Console.ReadLine();