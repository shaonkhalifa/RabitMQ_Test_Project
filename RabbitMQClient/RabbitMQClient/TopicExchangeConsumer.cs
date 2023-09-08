using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQClient
{
    public class TopicExchangeConsumer
    {
        public static void Consumer(IModel channel)
        {
            channel.ExchangeDeclare("topicexchange", ExchangeType.Topic);

            channel.QueueDeclare(queue: "topicqueue",
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

            channel.QueueBind("topicqueue", "topicexchange", "key.*");

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                Console.WriteLine($"Message -> Using Topic Exchange ->  = {message}");
            };

            channel.BasicConsume(queue: "topicqueue",
                                autoAck: true,
                                consumer: consumer);

            Console.ReadLine();
        }
    }
}
