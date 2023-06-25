namespace CleanArchitecture.Domain.Common;

public interface IAggregateRoot
{
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
    IReadOnlyCollection<IIntegrationEvent> IIntegrationEvents { get; }
    void ClearDomainEvents();
    void ClearIntegrationEvents();
}
