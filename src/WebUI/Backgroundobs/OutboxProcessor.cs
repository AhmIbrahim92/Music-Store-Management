using CleanArchitecture.Domain.Events;
using CleanArchitecture.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace WebUI.Backgroundobs;

public class OutboxProcessor : BackgroundService
{
    private readonly IMediator _mediator;
    private readonly IServiceScopeFactory _factory;
    private readonly ILogger<OutboxProcessor> _logger;
    private readonly IEventSerializer _eventSerializer;
    public OutboxProcessor(IMediator mediator, IServiceScopeFactory factory, ILogger<OutboxProcessor> logger, IEventSerializer eventSerializer)
    {
        _mediator = mediator ?? throw new ArgumentNullException();
        _factory = factory ?? throw new ArgumentNullException(); ;
        _logger = logger;
        _eventSerializer = eventSerializer;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using PeriodicTimer timer = new PeriodicTimer(TimeSpan.FromSeconds(2));
        while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken))
        {
            try
            {

                await using AsyncServiceScope asyncScope = _factory.CreateAsyncScope();
                ApplicationDbContext dbContext = asyncScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                var outBoxMessages = await dbContext.OutBoxMessages.Where(x => x.ProcessedAt == null && !string.IsNullOrEmpty(x.Payload)).ToListAsync();
                foreach (var outBoxMessage in outBoxMessages)
                {
                    var @event = _eventSerializer.Deserialize(outBoxMessage.Payload,outBoxMessage.Type);
                    await _mediator.Publish(@event);
                    outBoxMessage.MarkAsProceeded();
                }
                await dbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                _logger.LogInformation(
                    $"Failed to process Outbox M<essages with exception message {ex.Message}. Good luck next round!");
            }
        }
    }
}
