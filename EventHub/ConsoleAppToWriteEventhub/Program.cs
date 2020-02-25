using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;

namespace ConsoleAppToWriteEventhub
{
    class Program
    {
        private const string connectionstring_eventhubnamespace = "Endpoint=sb://testeventhub9870.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=QfiTpNH7DxJjBoMp3jQ2Dh0KgMu9AI6oXszsq7Kqoqo=";
        private const string eventhubname = "testeventhub";
        static async Task Main(string[] args)
        {// Create a producer client that you can use to send events to an event hub
            await using (var producerClient = new EventHubProducerClient(connectionstring_eventhubnamespace, eventhubname))
            {
                // Create a batch of events 
                using EventDataBatch eventBatch = await producerClient.CreateBatchAsync();

                // Add events to the batch. An event is a represented by a collection of bytes and metadata. 
                eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes("First event")));
                eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes("Second event")));
                eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes("Third event")));

                // Use the producer client to send the batch of events to the event hub
                await producerClient.SendAsync(eventBatch);
                Console.WriteLine("A batch of 3 events has been published.");
            }
        }
    }
}
