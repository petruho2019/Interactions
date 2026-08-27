namespace Not.RabbitMq.Helpers.Abstracts;


public interface IRabbitHelper
{
    public Task PublishMessage<T>(T message, CancellationToken cancellationToken = default) where T : new();
}