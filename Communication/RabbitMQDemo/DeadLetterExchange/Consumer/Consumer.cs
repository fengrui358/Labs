﻿using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Consumer
{
    internal class Consumer
    {
        private const string DEAD_EXCHANGE_NAME = "DEAD_EXCHANGE_NAME";
        private const string DEAD_QUEUE_NAME = "DEAD_QUEUE_NAME";

        private const string _exchangeName = "dl_exchange";
        private const string _queueName = "dl_queue";
        private const string _bindingKey = "dl_key";
        private const string _routingKey = "dl_key";
        private IConnection _connection;

        public Consumer()
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                Port = 5672,
                VirtualHost = "test",
                UserName = "testUser",
                Password = "abc123",
                AutomaticRecoveryEnabled = true
            };

            _connection = factory.CreateConnection();

            CreateDeadQueue();
        }

        public void Run()
        {
            var channel = _connection.CreateModel();
            // channel.ExchangeDeclare(_exchangeName, ExchangeType.Direct, true, false, null);
            channel.QueueDeclare(_queueName, true, false, false,
                new Dictionary<string, object>
                {
                    { "x-message-ttl", 5000 }, //过期时间5s
                    { "x-dead-letter-exchange", DEAD_EXCHANGE_NAME }, //死信队列交换机
                    { "x-dead-letter-routing-key", DEAD_QUEUE_NAME } //死信队列routingKey
                });
            channel.QueueBind(_queueName, _exchangeName, _bindingKey, null);
            // 消费死信队列
            channel.QueueBind(DEAD_QUEUE_NAME, DEAD_EXCHANGE_NAME, DEAD_QUEUE_NAME, null);
            //channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: true);

            //消息持久化
            var properties = channel.CreateBasicProperties();
            properties.Persistent = true; //相当于设置DeliveryMode=2

            // 订阅普通队列
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"{DateTime.Now} [x] Received {ea.Exchange} -- {ea.DeliveryTag} -- {message}");

                // 1. 第一种情况，消息处理者拒绝 ack 消息，并且不重新入队
                channel.BasicNack(ea.DeliveryTag, false, false);
            };

            // 2. 不消费普通队列，则消息在消息队列中存活时间超过 ttl 就会转发到死信队列
            // 注释下面的代码取消消费普通队列
            //channel.BasicConsume(queue: _queueName,
            //    autoAck: false, // autoAck false
            //    consumer: consumer);
            channel.BasicConsume(queue: DEAD_QUEUE_NAME,
                autoAck: false, // autoAck false
                consumer: consumer);
        }

        /// <summary>
        /// 创建死信交换机、死信队列、并绑定
        /// 死信交换机、队列就是一个普通交换机、队列
        /// </summary>
        private void CreateDeadQueue()
        {
            using (var channel = _connection.CreateModel())
            {
                channel.ExchangeDeclare(DEAD_EXCHANGE_NAME, ExchangeType.Direct, true, false, null);
                channel.QueueDeclare(DEAD_QUEUE_NAME, true, false, false, null);
                channel.QueueBind(DEAD_QUEUE_NAME, DEAD_EXCHANGE_NAME, DEAD_QUEUE_NAME, null);
            }
        }
    }
}
