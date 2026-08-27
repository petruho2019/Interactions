

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using RabbitMQInteraction.Common;

namespace RabbitMQInteraction;

public partial class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var services = builder.Services;
        var conf = builder.Configuration;

        services.AddControllers();

        // services.AddRabbit(conf);

        var app = builder.Build();

        app.MapControllers();

        // app.MapGet("/send-message", async ([FromQuery] string message, RabbitHelper rabbitHelper, HttpContext context, CancellationToken ct) =>
        // {
        //     await rabbitHelper.SendMessage(message);
        // });

        app.Run();
    }
}