using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RabitMQServer
{
    public class HeaderExchangePublisher
    {
        public static void Publisher(IModel channel)
        {
            channel.ExchangeDeclare("headerexchange", ExchangeType.Headers);

            var properties = channel.CreateBasicProperties();
            properties.Headers = new Dictionary<string, object>() { { "priority", "high" } };

            int counter = 1;
            while (counter <= 10)
            {
                var message = new { Name = "This Message From Header Exchange", Count = counter };

                var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

                channel.BasicPublish("headerexchange",
                                    routingKey: "key.hello",
                                    basicProperties: properties,
                                    body: body);

                Console.WriteLine($"Message Number {counter} Sent Successfully ");
                counter++;
            }
        }
    }
}
