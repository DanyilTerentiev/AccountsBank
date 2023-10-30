using MassTransit;
using Microsoft.Extensions.Logging;

namespace AccountsBank.BLL.Consumers;

public class UserDeletedConsumer : IConsumer<UserDeletedEvent>
{
    private readonly ILogger<UserDeletedConsumer> _logger;

    public UserDeletedConsumer(ILogger<UserDeletedConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<UserDeletedEvent> context)
    {
        Console.WriteLine(context.Message.ToString());

        _logger.LogInformation(context.Message.ToString());
        return Task.CompletedTask;
    }
}