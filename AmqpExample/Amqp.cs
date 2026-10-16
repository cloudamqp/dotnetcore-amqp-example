using RabbitMQ.Client;

namespace AmqpExample;

public static class Amqp
{
    // Automatic recovery is on by default: the connection, channels and consumers are restored after a network failure
    public static Task<IConnection> ConnectAsync()
    {
        var url = Environment.GetEnvironmentVariable("CLOUDAMQP_URL") ?? "amqp://guest:guest@localhost";
        var factory = new ConnectionFactory { Uri = new Uri(url) };
        return factory.CreateConnectionAsync();
    }
}
