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

            var filterKey = "";

            using var connection = factory.CreateConnection();
            Listen(connection, "ErExchangeTopic", filterKey);
            Listen(connection, "ErExchangeFanout", filterKey);
            Listen(connection, "SyncDataExchangeTopic", filterKey);

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }

        private static IModel Listen(IConnection connection, string exchangeName, string filterKey = "", string routingKey = "#")
        {
            var queueName = $"{exchangeName}_queue";
            var channel = connection.CreateModel();
            channel.QueueDeclare(queue: queueName,
                durable: false,
                exclusive: false,
                autoDelete: true,
                arguments: null);

            channel.QueueBind(queueName, exchangeName, routingKey);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                if (message.Contains(filterKey))
                {
                    Console.WriteLine($" [x] Received {exchangeName} {message}");
                }
            };
            channel.BasicConsume(queue: queueName,
                autoAck: true,
                consumer: consumer);

            return channel;
        }
    }
}
