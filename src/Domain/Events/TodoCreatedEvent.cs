namespace CleanArchitecture.Domain.Events;

public class TodoCreatedEvent : BaseEvent,IIntegrationEvent
{
    public TodoCreatedEvent(string title)
    {
        Title = title;
    }

    public string Title { get; }
}
