using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using System.Text.Json;

namespace EduSyncAPI.Helpers
{
    public class EventHubPublisher
    {
        private readonly string _connectionString;
        private readonly string _eventHubName;

        public EventHubPublisher(string connectionString, string eventHubName)
        {
            _connectionString = connectionString;
            _eventHubName = eventHubName;
        }

        public async Task PublishAsync<T>(T data)
        {
            await using var producerClient = new EventHubProducerClient(_connectionString, _eventHubName);
            using EventDataBatch eventBatch = await producerClient.CreateBatchAsync();

            var jsonData = JsonSerializer.SerializeToUtf8Bytes(data);
            var eventData = new EventData(jsonData);
            if (!eventBatch.TryAdd(eventData))
                throw new Exception("Failed to add event data to batch.");

            await producerClient.SendAsync(eventBatch);
        }
    }
}
