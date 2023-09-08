using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RabitMQServer
{
    public class DirectExchangePublisher
    {
        public static void Publisher(IModel channel)
        {
            var arg = new Dictionary<string, object>()
            {

                { "secret-key", 1258963 },
                { "secret-path", 4 },
                { "secret-header", 9}
            };
            channel.ExchangeDeclare("DirectExchange", ExchangeType.Direct, arguments: arg);
            int counter = 0;
            while (true)
            {
                var message = new { Name = "This Message From Direct Exchange", Count = counter };

                var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

                channel.BasicPublish("DirectExchange",
                                    routingKey: "key-direct-route",
                                    basicProperties: null,
                                    body: body);

                Console.WriteLine($"Message Number {counter} Sent Successfully ");
                counter++;
            }
        }
    }
}
