using System;
using System.Text;
using System.Threading;
using RabbitMQ.Client;

namespace dotnetcore_amqp_example
{
    class Program
    {
        // The url of the AMQP server, should look like amqps://[username]:[password]@[instance]/[vhost]
        static readonly string _url = "amqp://guest:guest@localhost/%2f";
        static ManualResetEvent _quitEvent = new ManualResetEvent(false);

        public static int Main(string[] args)
        {
            var url = _url;
            if (args.Length > 0)
                url = args[0];

            Console.CancelKeyPress += (sender, eArgs) => {
                // set the quit event so that the Consumer will recieve it and quit gracefully
                _quitEvent.Set();
                Console.WriteLine("CancelEvent recieved, shutting down...");
                // sleep 1 second to give Consumer time to clean up
                Thread.Sleep(1000);
            };

            // setup worker thread
            var consumer = new Consumer(url, _quitEvent);
            var consumerThread = new Thread(consumer.ConsumeQueue) { IsBackground = true };
            consumerThread.Start();

            // create a connection and open a channel, dispose them when done
            var factory = new ConnectionFactory
            {
                Uri = new Uri(url)
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            // ensure that the queue exists before we publish to it
            var queueName = "queue1";
            bool durable = false;
            bool exclusive = false;
            bool autoDelete = true;

            channel.QueueDeclare(queueName, durable, exclusive, autoDelete, null);

            while (true)
            {
                Console.WriteLine("Enter a message and publish with pressing the return key (exit with ctrl-c)");
                var message = Console.ReadLine();
                // the data put on the queue must be a byte array
                var data = Encoding.UTF8.GetBytes(message);
                // publish to the "default exchange", with the queue name as the routing key
                var exchangeName = "";
                var routingKey = queueName;
                channel.BasicPublish(exchangeName, routingKey, null, data);
                Console.WriteLine("Published message {0}", message);
            }
        }
    }
}
