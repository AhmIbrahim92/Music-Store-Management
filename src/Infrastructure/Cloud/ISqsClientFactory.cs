using Amazon.SQS;

namespace CleanArchitecture.Infrastructure.Cloud;

public interface ISqsClientFactory {
    string GetSqsQueue();
    IAmazonSQS GetSqsClient();
}

