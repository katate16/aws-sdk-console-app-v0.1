using Amazon.SimpleNotificationService;
using Amazon.SQS;
using aws_sdk_console_app_v0._1.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace aws_sdk_console_app_v0._1 {
    class Program {
        static void Main(string[] args) {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var serviceProvier = serviceCollection.BuildServiceProvider();
            
            Console.WriteLine("Hello World!");
        }

        public static void ConfigureServices(IServiceCollection services) {
            services.AddSingleton<IAmazonSimpleNotificationService, AmazonSimpleNotificationServiceClient>();
            services.AddSingleton<AmazonSQSClient, AmazonSQSClient>();
            
            services.AddSingleton<ISnsService, SnsService>()
                .AddScoped<AmazonSQSClient, AmazonSQSClient>();
        }
    }
}
