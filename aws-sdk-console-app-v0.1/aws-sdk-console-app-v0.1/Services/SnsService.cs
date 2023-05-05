using System;
using System.Threading.Tasks;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;

namespace aws_sdk_console_app_v0._1.Services {
    public class SnsService {
        private static IAmazonSimpleNotificationService _notificationService;
        private static SqsService _sqsService;

        public SnsService(IAmazonSimpleNotificationService notificationService, SqsService sqsService) {
            _notificationService = notificationService;
            _sqsService = sqsService;
        }

        /// <summary>
        /// This method calls the Notification Service to create a SNS Topic
        /// <returns>
        /// A CreateTopicResponse Object.
        /// </returns>
        /// </summary>
        /// <param name="topicName">The Topic's name.</param>
        public static async Task<CreateTopicResponse> CreateSnsTopicAsync(string topicName) {
            var request = new CreateTopicRequest() {
                Name = topicName
            };

            try {
                return await _notificationService.CreateTopicAsync(request);
            }
            catch (Exception ex) {
                Console.WriteLine($"Failed to create Topic {topicName} : Error Type:[{ex.GetType()}] Message:[{ex.Message}]");
                return new CreateTopicResponse {
                    HttpStatusCode = System.Net.HttpStatusCode.InternalServerError
                };
            }
        }

        /// <summary>
        /// This method calls the Notification Service to delete a SNS Topic
        /// <returns>
        /// A DeleteTopicResponse Object.
        /// </returns>
        /// </summary>
        /// <param name="topicArn">The Topic's ARN.</param>
        public static async Task<DeleteTopicResponse> DeleteTopicAsync(string topicArn) {
            var request = new DeleteTopicRequest() {
                TopicArn = topicArn
            };

            try {
                return await _notificationService.DeleteTopicAsync(request);
            }
            catch (Exception ex) {
                Console.WriteLine($"Failed to delete Topic {topicArn}: Error Type:[{ex.GetType()}] Message:[{ex.Message}]");
                return new DeleteTopicResponse {
                    HttpStatusCode = System.Net.HttpStatusCode.InternalServerError
                };
            }
        }

        /// <summary>
        /// This method calls the Notification Service to Publish a message to a given SNS Topic
        /// <returns>
        /// A PublishResponse Object.
        /// </returns>
        /// </summary>
        /// <param name="topicArn">The Topic's ARN.</param>
        /// <param name="message">The Message to be published.</param>
        public static async Task<PublishResponse> PublishToTopicAsync(string topicArn, string message) {
            var request = new PublishRequest() {
                Message = message,
                TopicArn = topicArn
            };

            try {
                return await _notificationService.PublishAsync(request);
            }
            catch (Exception ex) {
                Console.WriteLine($"Failed to publish message to Topic ({topicArn}) : Error Type:[{ex.GetType()}] Message:[{ex.Message}]");
                return new PublishResponse {
                    HttpStatusCode = System.Net.HttpStatusCode.InternalServerError
                };
            }
        }

        /// <summary>
        /// This method calls the Notification Service to Subscribe to a given SNS Topic
        /// <returns>
        /// A SubscribeResponse Object.
        /// </returns>
        /// </summary>
        /// <param name="topicArn">The Topic's ARN.</param>
        public static async Task<SubscribeResponse> SubscribeToTopicAsync(string topicArn) {
            var request = new SubscribeRequest() {
                TopicArn = topicArn,
            };

            try {
                return await _notificationService.SubscribeAsync(request);
            }
            catch (Exception ex) {
                Console.WriteLine($"Failed to subscribe to Topic ({topicArn}) : Error Type:[{ex.GetType()}] Message:[{ex.Message}]");
                return new SubscribeResponse {
                    HttpStatusCode = System.Net.HttpStatusCode.InternalServerError
                };
            }
        }

        /// <summary>
        /// This method calls the Notification Service to Subscribe a SQS Queue to a given SNS Topic
        /// <returns>
        /// A SubscribeResponse Object.
        /// </returns>
        /// </summary>
        /// <param name="sqsQueueUrl">The SQS Queue Url.</param>
        /// <param name="topicArn">The Topic's ARN.</param>
        public static async Task<SubscribeResponse> SubscribeQueue(string sqsQueueUrl, string topicArn) {
            var queueArn = await SqsService.GetQueueArn(sqsQueueUrl);

            var request = new SubscribeRequest() {
                Endpoint = queueArn
            };

            try {
                var response = !string.IsNullOrWhiteSpace(queueArn) ?
                    await _notificationService.SubscribeAsync(request)
                    : new SubscribeResponse { HttpStatusCode = System.Net.HttpStatusCode.BadRequest };

                return response;
            }
            catch (Exception ex) {
                Console.WriteLine($"Failed to subscribe Queue {sqsQueueUrl} to Topic ({topicArn}) : Error Type:[{ex.GetType()}] Message:[{ex.Message}]");
                return new SubscribeResponse {
                    HttpStatusCode = System.Net.HttpStatusCode.InternalServerError
                };
            }
        }
    }
}

