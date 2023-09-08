using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQClient
{
    public class HeaderExchangeConsumer
    {
        public static void Consumer(IModel channel)
        {
            channel.ExchangeDeclare("headerexchange", ExchangeType.Headers);
            channel.QueueDeclare(queue: "headersqueue",
                                durable: true,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);

            var arguments = new Dictionary<string, object>
        {
            { "x-match", "any" },
            { "priority", "high" }
        };

            channel.QueueBind("headersqueue", "headerexchange", string.Empty, arguments:arguments);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                Console.WriteLine($"Message -> Using Header Exchange ->  = {message}");
            };

            channel.BasicConsume(queue: "headersqueue",
                                autoAck: true,
                                consumer: consumer);

            Console.ReadLine();
        }
    }
}
