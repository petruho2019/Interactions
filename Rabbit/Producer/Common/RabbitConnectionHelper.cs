


using RabbitMQ.Client;
using Microsoft.Extensions.Options;
using RabbitMQ.Client.Exceptions;
using RabbitMQInteraction.Models.Rabbit;

namespace RabbitMQInteraction.Common;

public class RabbitChannelHelper
{
    private readonly SemaphoreSlim _connectionLock = new(1, 1);

    private ConnectionFactory _connFactory;
    private IConnection _conn;
    private IChannel _channel;
    private ILogger<RabbitChannelHelper> _logger;
    public RabbitChannelHelper(IOptions<RabbitConf> conf, ILogger<RabbitChannelHelper> logger)
    {

        var confVal = conf.Value;
        System.Console.WriteLine(confVal.ToString());

        _connFactory = new ConnectionFactory() { HostName = confVal.Host, UserName = confVal.Username, Password = confVal.Password };
        _logger = logger;
    }

    public async Task<IChannel> GetOrCreateChannel()
    {
        if (_conn?.IsOpen == true)
            return _channel;

        await EnsureConnectionOpen();
        await EnsureQueueCreated();

        return _channel;
    }

    private async Task EnsureQueueCreated()
    {
        if (!string.IsNullOrEmpty(_channel.CurrentQueue)) return;

        await CreateQueue();
    }

    private async Task CreateQueue()
    {
        await _channel.QueueDeclareAsync(queue: RabbitConstants.QUEUE_NAME, durable: true, exclusive: false, autoDelete: false,
            arguments: new Dictionary<string, object?> { { "x-queue-type", "quorum" } });
    }

    private async Task EnsureConnectionOpen()
    {
        await _connectionLock.WaitAsync();

        try
        {
            if (_conn?.IsOpen == true)
                return;

            _conn?.Dispose();
            _channel?.Dispose();

            _conn = await _connFactory.CreateConnectionAsync();
            _channel = await _conn.CreateChannelAsync();

        }
        catch (RabbitMQClientException ex)
        {
            _logger.LogError($"An exception occurred while opening the connection. Message: {ex.Message} ");

            _conn?.Dispose();     // Нужено ли он тут?
            _channel?.Dispose();

            throw;
        }
        finally
        {
            _connectionLock.Release();
        }
    }
}