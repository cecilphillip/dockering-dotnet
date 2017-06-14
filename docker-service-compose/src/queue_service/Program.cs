using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Polly;
using RabbitMQ.Client.Exceptions;

namespace queue_service
{
    class Program
    {
        public static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var rabbitSettings = configuration.GetSection("rabbitmq-settings");

            var config = new QueueConfig
            {
                HostName = rabbitSettings["hostName"],
                UserName = rabbitSettings["userName"],
                Password = rabbitSettings["password"],
                QueueName = rabbitSettings["sendQueue"]
            };

            DisplayRabbitSettings(config);
            Console.WriteLine("Starting School Service Queue Processor....");
            Console.WriteLine();

            // Rabbitmq startup is slow. Add retry w/ backoff
            var policy = Policy.Handle<BrokerUnreachableException>()
                  .WaitAndRetry(3,
                      attempt => TimeSpan.FromSeconds(attempt * 3),
                      (exp, timeSpan) =>
                      {
                          Console.WriteLine($"Retrying {nameof(queue_service)}");
                      }
                  );

            policy.Execute(() =>
            {
                var processor = new QueueProcessor(config);
                processor.Start();
            });
        }

        private static void DisplayRabbitSettings(QueueConfig config)
        {
            Console.WriteLine("*********************");
            Console.WriteLine("Host: {0}", config.HostName);
            Console.WriteLine("Username: {0}", config.UserName);
            Console.WriteLine("Password: {0}", config.Password);
            Console.WriteLine("QueueName: {0}", config.QueueName);
            Console.WriteLine("*********************");
            Console.WriteLine();
        }
    }
}
