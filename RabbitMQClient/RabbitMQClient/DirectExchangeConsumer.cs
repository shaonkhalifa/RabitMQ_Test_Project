using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQClient
{
    public class DirectExchangeConsumer
    {
        public static void Consumer(IModel channel)
        {
            channel.ExchangeDeclare("DirectExchange", ExchangeType.Direct, arguments: null);

            channel.QueueDeclare(queue: "directqueue",
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

            channel.QueueBind("directqueue", "DirectExchange", "key-direct-route");

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                Console.WriteLine($"Message -> Using Direct Exchange -> = {message}");
            };

            channel.BasicConsume(queue: "directqueue",
                                autoAck: true,
                                consumer: consumer);

            Console.ReadLine();
        }
    }
}
