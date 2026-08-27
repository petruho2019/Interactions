



using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace RabbitMQInteraction.Common;

public class RabbitHelper(RabbitChannelHelper connHelper)
{
    public async Task SendMessage<T>(T message)
    {
        var channel = await connHelper.GetOrCreateChannel();

        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

        await channel.BasicPublishAsync<BasicProperties>(exchange: string.Empty, mandatory: true, routingKey: RabbitConstants.QUEUE_NAME, basicProperties: new(), body: body);
    }

}