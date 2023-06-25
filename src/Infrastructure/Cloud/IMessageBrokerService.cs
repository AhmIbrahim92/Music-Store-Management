using Common.Models;

namespace CleanArchitecture.Infrastructure.Cloud;

public interface IMessageBrokerService
{
    Task<IEnumerable<TodoCreatedModel>> GetToDoItemsAsync();
    Task PublishToDoItemAsync(TodoCreatedModel item);

}