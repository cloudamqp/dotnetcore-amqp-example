# Use amqp in dotnet

This project shows how to use the [rabbitmq-dotnet-client](https://github.com/rabbitmq/rabbitmq-dotnet-client) to publish and consume messages in a dotnet console application.
It consists of a [Producer](https://github.com/cloudamqp/dotnetcore-amqp-example/blob/main/dotnetcore-amqp-example/Program.cs) and a [Consumer](https://github.com/cloudamqp/dotnetcore-amqp-example/blob/main/dotnetcore-amqp-example/Consumer.cs) 
that runs in a separate thread.

## Usage

1. Clone the project
2. cd into the project folder
3. Run the project (no args will run using the defaul URL amqp://guest:guest@localhost)
4. dotnet run --project dotnetcore-amqp-example -- amqps://[username]:[password]@[instance]/[vhost]
