# Use CloudAMQP from .NET

A minimal example of publishing and consuming messages with the
[RabbitMQ .NET client](https://github.com/rabbitmq/rabbitmq-dotnet-client) 7, against
[CloudAMQP](https://www.cloudamqp.com) or any AMQP 0-9-1 broker such as
[LavinMQ](https://lavinmq.com) or RabbitMQ.

- [`Publisher.cs`](AmqpExample/Publisher.cs) publishes a persistent message and waits for the broker's confirm.
- [`Worker.cs`](AmqpExample/Worker.cs) consumes with manual acknowledgements and a prefetch of 10.
- [`Amqp.cs`](AmqpExample/Amqp.cs) connects using the `CLOUDAMQP_URL` environment variable, and recovers automatically after network failures.

## Usage

Tool versions are in `mise.toml`; install them with [mise](https://mise.jdx.dev),
or use the .NET 10 SDK.

```sh
export CLOUDAMQP_URL="amqps://user:password@host/vhost"
dotnet run --project AmqpExample -- worker    # in one terminal
dotnet run --project AmqpExample -- publish   # in another
```

Without `CLOUDAMQP_URL` it connects to `amqp://guest:guest@localhost`.

## Tests

`dotnet test` publishes and consumes a message through a real broker on
localhost. CI runs it against LavinMQ.
