using System.Text.Json;
using MassTransit;
using Not.RabbitMq.Helpers.Abstracts;

namespace Not.RabbitMq.Helpers.Concrete;

public class RabbitHelper(IBus bus) : IRabbitHelper
{
    public async Task PublishMessage<T>(T message, CancellationToken cancellationToken = default) where T : new()
    {
        var serializedMessage = JsonSerializer.Serialize(message);
        await bus.Publish(serializedMessage, cancellationToken);
    }
}