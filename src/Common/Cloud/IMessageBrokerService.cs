using Common.Models;

namespace CleanArchitecture.Common.Cloud;

public interface IMessageBrokerService
{
    Task<IEnumerable<TodoCreatedModel>> GetToDoItemsAsync();
    Task PublishToDoItemAsync(TodoCreatedModel item);

}