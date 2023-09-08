using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RabitMQServer
{
    public class TopicExchangePublisher
    {
        public static void Publisher(IModel channel)
        {
            channel.ExchangeDeclare("topicexchange", ExchangeType.Topic);

            int counter = 1;
            while (counter <= 10)
            {
                var message = new { Name = "This Message From Topic Exchange", Count = counter };

                var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

                channel.BasicPublish("topicexchange",
                                    routingKey: "key.hello",
                                    basicProperties: null,
                                    body: body);

                Console.WriteLine($"Message Number {counter} Sent Successfully ");
                counter++;
            }
        }
    }
}
