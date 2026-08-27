using Confluent.Kafka;

namespace Consumer;

public class Program
{
    public static async Task Main(string[] args)
    {

        var config = new ConsumerConfig
        {
            BootstrapServers = "localhost:9092",
            GroupId = "sms",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        var ctSource = new CancellationTokenSource();

        ctSource.CancelAfter(300_000);

        var ct = ctSource.Token;

        using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
        consumer.Subscribe("sms");

        while (!ct.IsCancellationRequested)
        {
            var consumeResult = consumer.Consume(ct);

            System.Console.WriteLine(consumeResult.Message.Value);
        }

        System.Console.WriteLine("Закрываем консюмера");

        consumer.Close();
    }
}

