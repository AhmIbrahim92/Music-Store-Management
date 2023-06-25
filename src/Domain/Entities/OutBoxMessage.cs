namespace CleanArchitecture.Domain.Entities;

public class OutBoxMessage : BaseAuditableEntity
{
    //For EF Core
    protected OutBoxMessage() { }
    private OutBoxMessage(DateTime createdAt, string type, string payload)
    {
        this.CreatedAt = createdAt;
        this.Type = type;
        this.Payload = payload;
    }

    public DateTime CreatedAt { get; }
    public DateTime? ProcessedAt { get; private set; }
    public string Type { get; private set; }
    public string Payload { get; private set; }

    public void MarkAsProceeded()
    {
        this.ProcessedAt = DateTime.UtcNow;
    }

    public static OutBoxMessage FromIntegrationEvent<TE>(TE @event, IEventSerializer serializer) where TE : IIntegrationEvent
    {
        if (null == @event)
            throw new ArgumentNullException(nameof(@event));
        if (null == serializer)
            throw new ArgumentNullException(nameof(serializer));

        var type = @event.GetType().FullName;

        return new OutBoxMessage(DateTime.UtcNow, type, serializer.Serialize(@event));
    }
}
