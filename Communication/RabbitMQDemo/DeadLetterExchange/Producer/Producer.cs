using System.Text;
using RabbitMQ.Client;

namespace Producer
{
    internal class Producer
    {
        private const string DEAD_EXCHANGE_NAME = "DEAD_EXCHANGE_NAME";
        private const string DEAD_QUEUE_NAME = "DEAD_QUEUE_NAME";

        private const string _exchangeName = "dl_exchange";
        private const string _queueName = "dl_queue";
        private const string _bindingKey = "dl_key";
        private const string _routingKey = "dl_key";
        private IConnection _connection;

        public Producer()
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
            using (var channel = _connection.CreateModel())
            {
                channel.ExchangeDeclare(_exchangeName, ExchangeType.Direct, true, false, null);
                channel.QueueDeclare(_queueName, true, false, false,
                    new Dictionary<string, object> {
                        { "x-message-ttl", 5000 },//过期时间5s
                        { "x-dead-letter-exchange", DEAD_EXCHANGE_NAME },//死信队列交换机
                        { "x-dead-letter-routing-key", DEAD_QUEUE_NAME }//死信队列routingKey
                    });
                // channel.QueueBind(_queueName, _exchangeName, _bindingKey, null);

                //消息持久化
                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;//相当于设置DeliveryMode=2

                //5：发布消息
                for (int i = 0; i < 5; i++)
                {
                    var msg = $"{i}:haha";
                    Console.WriteLine($"send message to [{_exchangeName}] with routingKey [{_routingKey}]: {msg}");
                    channel.BasicPublish(_exchangeName, _routingKey, properties, Encoding.UTF8.GetBytes(msg));
                }
            }
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
