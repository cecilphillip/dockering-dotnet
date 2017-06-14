using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Polly;
using RabbitMQ.Client.Exceptions;

namespace service_client
{
    public class Program
    {
        private static IConfigurationRoot _configuration;
        private static ServiceClient _apiClient;
        public static void Main(string[] args)
        {
            LoadConfig();
            var policy = Policy.Handle<BrokerUnreachableException>()
                             .WaitAndRetry(3,
                                 attempt => TimeSpan.FromSeconds(attempt * 3),
                                 (exp, timeSpan) =>
                                 {
                                     Console.WriteLine($"Retrying {nameof(service_client)}");
                                 }
                             );

            var policyResult = policy.ExecuteAndCapture(() =>
             {
                 _apiClient = new ServiceClient(_configuration);
             });

            if (policyResult.Outcome == OutcomeType.Successful)
            {
                ListStudents();
                ListCourses();
                return;
            }

            Console.WriteLine("Unable to connect to RABBITMQ HOST");
        }

        private static void LoadConfig()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            _configuration = builder.Build();
        }

        private static void ListStudents()
        {
            var students = _apiClient.GetStudents();
            Console.WriteLine($"{students.Count()}");
        }

        private static void ListCourses()
        {
            var courses = _apiClient.GetCourses();
            Console.WriteLine($"{courses.Count()}");
        }
    }
}
