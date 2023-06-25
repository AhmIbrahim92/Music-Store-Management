using System.ComponentModel.DataAnnotations.Schema;

namespace CleanArchitecture.Domain.Common;

public abstract class BaseAggregateEntity : BaseAuditableEntity, IAggregateRoot
{
    public int Id { get; set; }

    private readonly List<IDomainEvent> _domainEvents = new();

    private readonly List<IIntegrationEvent> _integrationEvents= new();


    [NotMapped]
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    [NotMapped]
    public IReadOnlyCollection<IIntegrationEvent> IIntegrationEvents => _integrationEvents.AsReadOnly();

    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    public void AddIntegrationEvent(IIntegrationEvent integrationEvent)
    {
        _integrationEvents.Add(integrationEvent);
    }

    public void RemoveIntegrationEvent(IIntegrationEvent integrationEvent)
    {
        _integrationEvents.Remove(integrationEvent);
    }

    public void ClearIntegrationEvents()
    {
        _integrationEvents.Clear();
    }
}
