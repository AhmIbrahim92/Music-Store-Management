using System.Reflection;
using CleanArchitecture.Domain.Events;

namespace CleanArchitecture.Infrastructure.Persistence;

internal static class Assemblies
{
    public static readonly Assembly Application = typeof(TodoCreatedEvent).Assembly;
}