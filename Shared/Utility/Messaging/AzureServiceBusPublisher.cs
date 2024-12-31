using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using System.Text.Json;


namespace Utility.Messaging;

public class AzureServiceBusPublisher : IEventPublisher
{
    private readonly string _connectionString;

    public AzureServiceBusPublisher(IConfiguration configuration)
    {
        _connectionString = configuration["AzureServiceBus:ConnectionString"];
    }

    public async Task PublishAsync<T>(string topicName, T message)
    {
        await using var client = new ServiceBusClient(_connectionString);
        var sender = client.CreateSender(topicName);

        var jsonMessage = JsonSerializer.Serialize(message);
        var serviceBusMessage = new ServiceBusMessage(jsonMessage);

        await sender.SendMessageAsync(serviceBusMessage);
    }
}