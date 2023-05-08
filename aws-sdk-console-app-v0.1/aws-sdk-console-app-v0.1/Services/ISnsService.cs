using Amazon.SimpleNotificationService.Model;
using System.Threading.Tasks;

namespace aws_sdk_console_app_v0._1.Services {
    public interface ISnsService {
        public Task<CreateTopicResponse> CreateSnsTopicAsync(string topicName);

        public Task<DeleteTopicResponse> DeleteTopicAsync(string topicArn);

        public Task<PublishResponse> PublishToTopicAsync(string topicArn, string message);

        public Task<SubscribeResponse> SubscribeToTopicAsync(string topicArn);

        public Task<SubscribeResponse> SubscribeQueueAsync(string sqsQueueUrl, string topicArn);
    }
}
