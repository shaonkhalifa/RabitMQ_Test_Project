using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabitMQServer
{
    public class Program
    {
        static void Main(string[] args)
        {
            var factory= new ConnectionFactory() { HostName="localhost"};
            var connection = factory.CreateConnection();
            var channel= connection.CreateModel();

            //DirectExchangePublisher.Publisher(channel);
            //TopicExchangePublisher.Publisher(channel);
            //FanoutExchangePublisher.Publisher(channel);
            HeaderExchangePublisher.Publisher(channel);

            Console.WriteLine("Message Sending is completed.");
            Console.ReadKey();
        }
    }
}
