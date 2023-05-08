using Amazon.SQS.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace aws_sdk_console_app_v0._1.Services {
    public interface ISqsService {

        public Task<CreateQueueResponse> CreateQueueAsync(string queueName, Dictionary<string, string>? attributes);

        public Task<DeleteQueueResponse> DeleteQueueAsync(string queueUrl);

        public Task<string> GetQueueArn(string queueUrl);
    }
}
