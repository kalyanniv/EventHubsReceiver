using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;

namespace EventHubsReceiver
{
    class Program
    {
        static void Main(string[] args)
        {
            string eventHubConnectionString = "Endpoint=sb://testsvivekanhub-ns.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=LmeY4Ck7OGJ4cVjt1rc1a5MrZHlYBEyoLgqEY4NcqEo=";
            string eventHubName = "testsvivekanhub";
            string storageAccountName = "eventhubstoragesvivekan";
            string storageAccountKey = "Nsfx4Zul3yTQ/5I4/gM8FRU40snMvrSx1z8cCoU7oKOGbJNJlNAEZ6XBqUvRVQNKo3eZ2UYlT3ELciWzpIVcZg==";
            string storageConnectionString = string.Format("DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1}", storageAccountName, storageAccountKey);

            string eventProcessorHostName = Guid.NewGuid().ToString();
            EventProcessorHost eventProcessorHost = new EventProcessorHost(eventProcessorHostName, eventHubName, EventHubConsumerGroup.DefaultGroupName, eventHubConnectionString, storageConnectionString);
            Console.WriteLine("Registering EventProcessor...");
            var options = new EventProcessorOptions();
            options.ExceptionReceived += (sender, e) => { Console.WriteLine(e.Exception); };
            eventProcessorHost.RegisterEventProcessorAsync<SimpleEventProcessor>(options).Wait();

            Console.WriteLine("Receiving. Press enter key to stop worker.");
            Console.ReadLine();
            eventProcessorHost.UnregisterEventProcessorAsync().Wait();
        }
    }
}
