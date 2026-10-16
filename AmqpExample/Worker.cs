using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace AmqpExample;

public static class Worker
{
    // The message is acked after the handler has processed it
    public static async Task ConsumeAsync(IChannel channel, string queue, Func<string, Task> handler)
    {
        await channel.QueueDeclareAsync(queue, durable: true, exclusive: false, autoDelete: false);
        await channel.BasicQosAsync(prefetchSize: 0, prefetchCount: 10, global: false);

        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.ReceivedAsync += async (_, delivery) =>
        {
            await handler(Encoding.UTF8.GetString(delivery.Body.Span));
            await channel.BasicAckAsync(delivery.DeliveryTag, multiple: false);
        };
        await channel.BasicConsumeAsync(queue, autoAck: false, consumer);
    }
}
