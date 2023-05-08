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

        /// <summary>
        /// This method calls the sqsClient to create a SQS Queue
        /// <returns>
        /// A CreateQueueResponse Object.
        /// </returns>
        /// </summary>
        /// <param name="queueName">The Queue's name.</param>
        /// <param name="attributes">A Dictionary with the queue's attributes.</param>
        public async Task<CreateQueueResponse> CreateQueueAsync(string queueName, Dictionary<string, string>? attributes) {
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

        /// <summary>
        /// This method calls the sqsClient to Delete a SQS Queue
        /// <returns>
        /// A DeleteQueueResponse Object.
        /// </returns>
        /// </summary>
        /// <param name="queueUrl">The Queue's URL.</param>
        public async Task<DeleteQueueResponse> DeleteQueueAsync(string queueUrl) {
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

        /// <summary>
        /// This method calls the sqsClient to get the QueueARN
        /// <returns>
        /// The Queue ARN.
        /// </returns>
        /// </summary>
        /// <param name="queueUrl">The Queue's URL.</param>
        public async Task<string> GetQueueArn(string queueUrl) {
            var request = new GetQueueAttributesRequest() {
                QueueUrl = queueUrl,
                AttributeNames = new List<string>() { "QueueArn" }
            };

            try {
                var response = await _sQSClient.GetQueueAttributesAsync(request);
                return response.QueueARN;
            }
            catch (Exception ex) {
                Console.WriteLine($"Failed to delete Queue {queueUrl} : Error Type:[{ex.GetType()}] Message:[{ex.Message}]");
                return null;
            }
        }
    }
}
