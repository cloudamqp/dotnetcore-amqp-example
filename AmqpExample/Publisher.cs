using System.Text;
using RabbitMQ.Client;

namespace AmqpExample;

public static class Publisher
{
    // With confirmation tracking, BasicPublishAsync returns once the broker has confirmed the message
    public static async Task PublishAsync(IConnection connection, string queue, string message)
    {
        var options = new CreateChannelOptions(publisherConfirmationsEnabled: true, publisherConfirmationTrackingEnabled: true);
        await using var channel = await connection.CreateChannelAsync(options);
        await channel.QueueDeclareAsync(queue, durable: true, exclusive: false, autoDelete: false);

        var properties = new BasicProperties { Persistent = true };
        await channel.BasicPublishAsync("", queue, mandatory: true, properties, Encoding.UTF8.GetBytes(message));
        Console.WriteLine($" [x] Sent '{message}'");
    }
}
