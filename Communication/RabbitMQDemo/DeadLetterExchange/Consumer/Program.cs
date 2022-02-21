// See https://aka.ms/new-console-template for more information

using Consumer = Consumer.Consumer;

Console.WriteLine("接收死信队列");

var consumer = new global::Consumer.Consumer();
consumer.Run();


Console.ReadLine();
