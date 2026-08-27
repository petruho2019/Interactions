
using Not.Application.Hubs;
using Not.RabbitMq;
using Not.WebShared.Auth.Extensions;

namespace Not.WebApi;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.WebHost.ConfigureKestrel(opts =>
        {
            opts.ListenLocalhost(80);
        });

        var services = builder.Services;
        var conf = builder.Configuration;

        services.AddSimpleAuth();

        services.AddControllers();
        services.AddRabbitMq(conf);
        services.AddSignalR();

        services.AddCors(opts =>
        {
            opts.AddDefaultPolicy(policy =>
            {
                policy.SetIsOriginAllowed(origin =>
                {
                    if (origin == "http://localhost:4200")
                    {
                        return true;
                    }

                    return false;
                })
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials();
            });
        });

        var app = builder.Build();

        app.UseCors();

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapHub<NotificationHub>("/notificationHub");

        app.MapControllers();

        app.Run();
    }
}