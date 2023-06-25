namespace CleanArchitecture.Domain.Events;

public interface IEventSerializer
{
    string Serialize<TE>(TE @event) where TE : IIntegrationEvent;
    dynamic Deserialize(string payload, string type);
}