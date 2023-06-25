using CleanArchitecture.Common.Cloud;
using Common.Cloud.AWS;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace Common;

public static class ConfigureServices
{
    public static IServiceCollection AddMessageBrokerServices(this Microsoft.Extensions.DependencyInjection.IServiceCollection services, IConfiguration configuration)
    {

        services.Configure<SqsOptions>(options =>
        {
            options.SqsQueueId = configuration["SqsOptions:SqsQueueId"] ?? string.Empty;
            options.SqsQueueName = configuration["SqsOptions:SqsQueueName"] ?? string.Empty;
            options.SqsRegion = configuration["SqsOptions:SqsRegion"] ?? string.Empty;
            options.IamAccessKey = configuration["SqsOptions:IamAccessKey"] ?? string.Empty;
            options.IamSecretKey = configuration["SqsOptions:IamSecretKey"] ?? string.Empty;
        });

        services.AddTransient<IMessageBrokerService, MessageBrokerService>();
        services.AddSingleton<ISqsClientFactory, SqsClientFactory>();

        return services;
    }
}
