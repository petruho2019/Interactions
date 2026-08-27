using Confluent.Kafka;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();


app.MapGet("index", async (contex) =>
{
    System.Console.WriteLine("Отправляем в kafka сообщение");

    var cfg = new ProducerConfig(new ClientConfig()
    {
        BootstrapServers = "localhost:9092"
    });


    using var producer = new ProducerBuilder<Null, string>(cfg).Build();
    await producer.ProduceAsync("sms", new Message<Null, string>()
    {
        Value = "Hello from producer"
    }, default);

    System.Console.WriteLine("Отправили в kafka сообщение");
});

app.Run();

