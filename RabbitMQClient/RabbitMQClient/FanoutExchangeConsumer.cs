using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQClient
{
    public class FanoutExchangeConsumer
    {
        public static void Consumer(IModel channel)
        {
            var consumer = new EventingBasicConsumer(channel);

            channel.QueueDeclare(queue: "fanoutqueue",
                                 durable: true,    // will Survive after broker restart
                                 exclusive: false,  // false - define multiple connection to access this queue
                                 autoDelete: false, // will not be automatically deleted by the broker
                                 arguments: null);  // additional arguments such as configuration

            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                Console.WriteLine($"Message -> Using Fanout Exchange -> = {message}");
            };

            channel.BasicConsume(queue: "fanoutqueue",
                                autoAck: true,
                                consumer: consumer);

            Console.ReadLine();
        }
    }
}
