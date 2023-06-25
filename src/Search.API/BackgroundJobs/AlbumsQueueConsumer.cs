using CleanArchitecture.Common.Cloud;
using Microsoft.Extensions.DependencyInjection;

namespace Search.API.BackgroundJobs;

public class AlbumsQueueConsumer : BackgroundService
{
    private readonly ILogger<AlbumsQueueConsumer> _logger;
    private readonly IServiceScopeFactory _factory;
    public AlbumsQueueConsumer(ILogger<AlbumsQueueConsumer> logger, IServiceScopeFactory factory)
    {
        _logger = logger;
        _factory = factory;
    }


    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using PeriodicTimer timer = new PeriodicTimer(TimeSpan.FromSeconds(10));
        while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken))
        {
            await using AsyncServiceScope asyncScope = _factory.CreateAsyncScope();
            var messageProcessor =  asyncScope.ServiceProvider.GetRequiredService<IMessageBrokerService>();
            var messages = await messageProcessor.GetToDoItemsAsync();
            foreach (var item in messages)
            {
                _logger.LogInformation($"{item.Title}");
            }
        }
    }
}
