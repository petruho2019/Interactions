using System.Diagnostics;
using System.Net;
using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;

namespace Producer.Controllers;



[ApiController]
public class MainController() : ControllerBase
{


    [HttpGet]
    public async Task<IActionResult> Index()
    {


        return Ok();
    }

    private async Task<bool> SendOrderRequest
        (string topic, string message)
    {
        ProducerConfig config = new ProducerConfig
        {
            BootstrapServers = "localhost:9092",
            ClientId = Dns.GetHostName()
        };

        try
        {
            using var producer = new ProducerBuilder<Null, string>(config).Build();
            var result = await producer.ProduceAsync
            (topic, new Message<Null, string>
            {
                Value = message
            });

            Debug.WriteLine($"Delivery Timestamp: {result.Timestamp.UtcDateTime}");
            return await Task.FromResult(true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error occured: {ex.Message}");
        }

        return await Task.FromResult(false);
    }
}