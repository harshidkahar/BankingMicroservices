using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;

namespace Utility.Messaging;
public class AzureServiceBusSubscriber : IEventSubscriber
{
    private readonly string _connectionString;

    public AzureServiceBusSubscriber(IConfiguration configuration)
    {
        _connectionString = configuration["AzureServiceBus:ConnectionString"];
    }

    public async Task SubscribeAsync(string topicName, string subscriptionName, Func<string, Task> handleMessage)
    {
        await using var client = new ServiceBusClient(_connectionString);
        var processor = client.CreateProcessor(topicName, subscriptionName, new ServiceBusProcessorOptions());

        processor.ProcessMessageAsync += async args =>
        {
            try
            {
                var messageBody = args.Message.Body.ToString();
                await handleMessage(messageBody);
                await args.CompleteMessageAsync(args.Message);
            }
            catch (Exception)
            {
                await args.AbandonMessageAsync(args.Message);
            }
        };

        processor.ProcessErrorAsync += args =>
        {
            Console.WriteLine($"Error: {args.Exception.Message}");
            return Task.CompletedTask;
        };

        await processor.StartProcessingAsync();
    }
}
