using System.Reflection;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Not.RabbitMq;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddRabbitMq(IConfiguration conf, Assembly[]? assemblies = null)
        {
            services.AddMassTransit(x =>
            {
                x.SetKebabCaseEndpointNameFormatter();

                if (assemblies is not null)
                {
                    x.AddConsumers(assemblies);
                }

                x.UsingRabbitMq((context, config) =>
                {
                    var rabbitSection = conf.GetSection("RabbitMq");

                    config.Host(new Uri(rabbitSection["Host"]!), h =>
                    {
                        h.Username(rabbitSection["Username"]!);
                        h.Password(rabbitSection["Password"]!);
                    });
                });
            });

            return services;
        }

    }
}