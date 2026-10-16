using AmqpExample;

await using var connection = await Amqp.ConnectAsync();

switch (args.FirstOrDefault())
{
    case "publish":
        await Publisher.PublishAsync(connection, "hello", "Hello CloudAMQP!");
        break;
    case "worker":
        await using (var channel = await connection.CreateChannelAsync())
        {
            await Worker.ConsumeAsync(channel, "hello", message =>
            {
                Console.WriteLine($" [x] Received '{message}'");
                return Task.CompletedTask;
            });
            Console.WriteLine(" [*] Waiting for messages. To exit press CTRL+C");
            var exit = new TaskCompletionSource();
            Console.CancelKeyPress += (_, e) => { e.Cancel = true; exit.TrySetResult(); };
            await exit.Task;
        }
        break;
    default:
        Console.Error.WriteLine("Usage: dotnet run --project AmqpExample -- publish|worker");
        return 1;
}
return 0;
