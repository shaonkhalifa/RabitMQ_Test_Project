using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQClient
{
    public class Program
    {
        static void Main(string[] args)
        {
            ConnectionFactory factory = new ConnectionFactory() { HostName = "localhost", UserName = "guest", Password = "guest" };

             var connection = factory.CreateConnection();
             var channel = connection.CreateModel();

            //TopicExchangeConsumer.Consumer(channel);
            // DirectExchangeConsumer.Consumer(channel);
            //FanoutExchangeConsumer.Consumer(channel);
            HeaderExchangeConsumer.Consumer(channel);

            Console.ReadKey();
        }
    }
}
