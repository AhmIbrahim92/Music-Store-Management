using System.Text.Json.Serialization;
using MediatR;

namespace CleanArchitecture.Domain.Common;

public abstract class BaseEvent : INotification
{
    public BaseEvent()
    {

    }
}
