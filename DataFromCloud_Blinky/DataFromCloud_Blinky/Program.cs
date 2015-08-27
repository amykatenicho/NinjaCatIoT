using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;

namespace DataFromCloud_Blinky
{
    class Program
    {
        static void Main(string[] args)
        {
            string ehname = "<eventhubname>";
            string connection = "<eventhubconnectionstring>,TransportType=Amqp";
            MessagingFactory factory = MessagingFactory.CreateFromConnectionString(connection);
            EventHubClient ehub = factory.CreateEventHubClient(ehname);
            EventHubConsumerGroup group = ehub.GetDefaultConsumerGroup();
            EventHubReceiver reciever = group.CreateReceiver("0");

            while (true)
            {
                EventData data = reciever.Receive();
                if (data != null)
                {
                    try
                    {
                        string message = Encoding.UTF8.GetString(data.GetBytes());
                        //Console.WriteLine("Partition Key: {0}", data.PartitionKey);
                        Console.WriteLine(message);
                        Console.WriteLine();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
            }
        }
    }
}
