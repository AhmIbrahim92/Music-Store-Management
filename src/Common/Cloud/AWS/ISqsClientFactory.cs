using Amazon.SQS;

namespace Common.Cloud.AWS;

public interface ISqsClientFactory
{
    string GetSqsQueue();
    IAmazonSQS GetSqsClient();
}

