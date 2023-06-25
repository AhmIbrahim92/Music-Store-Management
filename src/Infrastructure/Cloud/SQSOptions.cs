using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Cloud;
public record SqsOptions
{
    public string SqsRegion { get; init; }
    public string SqsQueueId { get; init; }
    public string SqsQueueName { get; init; }
    public string IamAccessKey { get; init; }
    public string IamSecretKey { get; init; }
}
