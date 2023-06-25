using System.Text.Json;
using Amazon.SQS.Model;
using Common.Models;

namespace CleanArchitecture.Infrastructure.Cloud;

public class MessageBrokerService : IMessageBrokerService
{
    private readonly ISqsClientFactory _sqsClientFactory;

    public MessageBrokerService(ISqsClientFactory sqsClientFactory)
    {
        _sqsClientFactory = sqsClientFactory;
    }

    public async Task<IEnumerable<TodoCreatedModel>> GetToDoItemsAsync()
    {
        var messages = new List<TodoCreatedModel>();

        var request = new ReceiveMessageRequest
        {
            QueueUrl = _sqsClientFactory.GetSqsQueue(),
            MaxNumberOfMessages = 10,
            VisibilityTimeout = 10,
            WaitTimeSeconds = 10,
        };

        var response = await _sqsClientFactory.GetSqsClient().ReceiveMessageAsync(request);

        foreach (var message in response.Messages)
        {
            try
            {
                var m = JsonSerializer.Deserialize<TodoCreatedModel>(message.Body);
                if (m != null)
                    messages.Add(m);
            }
            catch
            {
                // Invalid message, ignore
            }
        }

        return messages;
    }

    public async Task PublishToDoItemAsync(TodoCreatedModel item)
    {
        var request = new SendMessageRequest
        {
            MessageBody = JsonSerializer.Serialize(item),
            QueueUrl = _sqsClientFactory.GetSqsQueue(),
        };

        var client = _sqsClientFactory.GetSqsClient();
        await client.SendMessageAsync(request);
    }
}
