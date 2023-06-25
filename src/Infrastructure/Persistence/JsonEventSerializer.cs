using System.Text.Json;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Domain.Events;
using Duende.IdentityServer.Events;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace CleanArchitecture.Infrastructure.Persistence;

public class JsonEventSerializer : IEventSerializer
{
    public string Serialize<TE>(TE @event) where TE : IIntegrationEvent
    {
        if (null == @event)
            throw new ArgumentNullException(nameof(@event));
        var eventType = @event.GetType();
        var result = JsonConvert.SerializeObject(@event);
        return result;
    }

    public dynamic Deserialize(string payload,string type) 
    {
        if (string.IsNullOrEmpty(payload))
            throw new ArgumentNullException(nameof(payload));
        dynamic @event = JsonConvert.DeserializeObject(payload, Assemblies.Application.GetType(type));

        return @event;
    }
}
