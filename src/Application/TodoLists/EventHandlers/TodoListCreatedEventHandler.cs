using CleanArchitecture.Common.Cloud;
using CleanArchitecture.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using Common.Models;

namespace CleanArchitecture.Application.TodoLists.EventHandlers;

public class TodoListCreatedEventHandler : INotificationHandler<TodoCreatedEvent>
{
    private readonly ILogger<TodoListCreatedEventHandler> _logger;
    private readonly IMessageBrokerService _messageBrokerService;

    public TodoListCreatedEventHandler(ILogger<TodoListCreatedEventHandler> logger, IMessageBrokerService messageBrokerService)
    {
        _logger = logger;
        _messageBrokerService = messageBrokerService;
    }

    public async Task Handle(TodoCreatedEvent notification, CancellationToken cancellationToken)
    {
        try
        {

            _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);
            await _messageBrokerService.PublishToDoItemAsync(new TodoCreatedModel("allo"));
        }
        catch (Exception ex)
        {

            throw;
        }
    }
}
