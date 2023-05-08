using Amazon;
using Amazon.Extensions.NETCore.Setup;
using Amazon.SimpleNotificationService;
using Amazon.SQS;
using aws_sdk_console_app_v0._1.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace aws_sdk_console_app_v0._1 {
    class Program {
        static void Main(string[] args) {
            var serviceCollection = new ServiceCollection();
            var serviceProvider = ConfigureServices(serviceCollection);
            Console.WriteLine("Hello World!");
        }

        public static ServiceProvider ConfigureServices(IServiceCollection services) {
            services.AddAWSService<AmazonSQSClient>();
            services.AddAWSService<IAmazonSimpleNotificationService>();

            services.AddSingleton<ISnsService, SnsService>();

            services.AddDefaultAWSOptions( 
                new AWSOptions {
                    Region = RegionEndpoint.USEast2,
                }
            );

            return services.BuildServiceProvider();
        }
    }
}
