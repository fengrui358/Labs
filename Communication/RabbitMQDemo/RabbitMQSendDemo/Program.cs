using System;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace RabbitMQSendDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory
            {
                HostName = "10.15.9.113", Port = 5672, VirtualHost = "CAD_CLIENT", UserName = "admin", Password = "admin",
                AutomaticRecoveryEnabled = true
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            //声明一个队列
            //channel.QueueDeclare(queue: "hello", durable: false, exclusive: false, autoDelete: true, arguments: null);

            Task.Run(async () =>
            {
                while (true)
                {
                    string message = "Hello World!";
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "CAD_CLIENT_MSG",
                        routingKey: "*.free",
                        basicProperties: null,
                        body: body);

                    Console.WriteLine(" [x] Sent {0}", message);

                    await Task.Delay(3000);
                }
            });

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
