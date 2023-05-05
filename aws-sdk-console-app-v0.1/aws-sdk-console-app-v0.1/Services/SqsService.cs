using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Amazon.SQS;
using Amazon.SQS.Model;

namespace aws_sdk_console_app_v0._1.Services {
    public class SqsService {
        private static AmazonSQSClient _sQSClient;

        public SqsService(AmazonSQSClient sQSClient) {
            _sQSClient = sQSClient;
        }

        public static async Task<CreateQueueResponse> CreateQueueAsync(string queueName, Dictionary<string, string>? attributes) {
            var request = new CreateQueueRequest() {
                QueueName = queueName,
                Attributes = attributes ?? new Dictionary<string, string>()
            };

            try {
                return await _sQSClient.CreateQueueAsync(request);
            }
            catch (Exception ex) {
                Console.WriteLine($"Failed to create Queue {queueName} : Error Type:[{ex.GetType()}] Message:[{ex.Message}]");
                return new CreateQueueResponse() {
                    HttpStatusCode = System.Net.HttpStatusCode.InternalServerError
                };
            }
        }

        public static async Task<DeleteQueueResponse> DeleteQueueAsync(string queueUrl) {
            var request = new DeleteQueueRequest() {
                QueueUrl = queueUrl
            };

            try {
                return await _sQSClient.DeleteQueueAsync(request);
            }
            catch (Exception ex) {
                Console.WriteLine($"Failed to delete Queue {queueUrl} : Error Type:[{ex.GetType()}] Message:[{ex.Message}]");
                return new DeleteQueueResponse() {
                    HttpStatusCode = System.Net.HttpStatusCode.InternalServerError
                };
            }
        }
    }
}
