using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Threading.Tasks;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;

namespace aws_sdk_console_app_v0._1.Services {
    public class SnsService {
        private static IAmazonSimpleNotificationService _notificationService;

        public SnsService(IAmazonSimpleNotificationService notificationService) {
            _notificationService = notificationService;
        }

        public static async Task<CreateTopicResponse> CreateSnsTopicAsync(string topicName) {
            var request = new CreateTopicRequest() {
                Name = topicName
            };

            try{
                return await _notificationService.CreateTopicAsync(request);
            }
            catch(Exception ex) {
                Console.WriteLine($"Failed to create Topic : Error Type:[{ex.GetType()}] Message:[{ex.Message}]");
                return new CreateTopicResponse {
                    HttpStatusCode = System.Net.HttpStatusCode.InternalServerError
                };
            }
        }

        public static async Task<DeleteTopicResponse> DeleteTopicAsync(string topicArn) {
            var request = new DeleteTopicRequest() {
                TopicArn = topicArn
            };

            try {
                return await _notificationService.DeleteTopicAsync(request);
            }
            catch(Exception ex) {
                Console.WriteLine($"Failed to delete Topic : Error Type:[{ex.GetType()}] Message:[{ex.Message}]");
                return new DeleteTopicResponse {
                    HttpStatusCode = System.Net.HttpStatusCode.InternalServerError
                };
            }
        }

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

        public static async Task<SubscribeResponse> SubscribeToTopicAsync(string topicArn) {
            var request = new SubscribeRequest() { 
                TopicArn = topicArn,
            };

            try {
                return await _notificationService.SubscribeAsync(request);
            }
            catch (Exception ex) {
                Console.WriteLine($"Failed to publish subscribe to Topic ({topicArn}) : Error Type:[{ex.GetType()}] Message:[{ex.Message}]");
                return new SubscribeResponse {
                    HttpStatusCode = System.Net.HttpStatusCode.InternalServerError
                };
            }

        }
    }
}
