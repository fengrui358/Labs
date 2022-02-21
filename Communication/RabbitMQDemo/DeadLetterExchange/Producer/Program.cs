// See https://aka.ms/new-console-template for more information

using Producer = Producer.Producer;

Console.WriteLine("死信队列测试");

var producer = new global::Producer.Producer();
producer.Run();

Console.ReadLine();
