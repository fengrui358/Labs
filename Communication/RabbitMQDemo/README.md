# RabbitMQ

## 死信队列

参考：<https://www.jb51.net/article/221796.htm>

1. 消息消费者手动拒绝消息，并且不再将消息入队：`channel.BasicNack(ea.DeliveryTag, false, requeue: false);`
