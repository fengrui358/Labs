using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQReceiveDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                Port = 44465,
                VirtualHost = "er",
                UserName = "eruser",
                Password = "",
                AutomaticRecoveryEnabled = true
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: "free",
                durable: false,
                exclusive: false,
                autoDelete: true,
                arguments: null);

            channel.QueueBind("free", "ErExchangeTopic", "#");

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(" [x] Received Topic {0}", message);
            };
            channel.BasicConsume(queue: "free",
                autoAck: true,
                consumer: consumer);

            using var channel2 = connection.CreateModel();
            channel2.QueueDeclare(queue: "free2",
                durable: false,
                exclusive: false,
                autoDelete: true,
                arguments: null);

            channel2.QueueBind("free2", "ErExchangeFanout", "#");

            var consumer2 = new EventingBasicConsumer(channel2);
            consumer2.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(" [x] Received Topic {0}", message);
            };
            channel2.BasicConsume(queue: "free2",
                autoAck: true,
                consumer: consumer);

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
