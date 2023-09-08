using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RabitMQServer
{
    public class FanoutExchangePublisher
    {

        public static void Publisher(IModel channel)
        {
            channel.ExchangeDeclare("fanoutexchange", ExchangeType.Fanout);

            

            channel.QueueBind("fanoutqueue", "fanoutexchange", string.Empty);
            int counter = 1;
            while (counter <= 500)
            {
                var message = new { Name = "This Message From Fanout Exchange", Count = counter };

                var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

                channel.BasicPublish("fanoutexchange",
                                    routingKey: string.Empty,
                                    basicProperties: null,
                                    body: body);

                Console.WriteLine($"Message Number {counter} Sent Successfully ");
                counter++;
            }
        }
    }
}
