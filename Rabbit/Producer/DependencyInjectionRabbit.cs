




using RabbitMQInteraction.Common;
using RabbitMQInteraction.Models.Rabbit;

public static class DependencyInjectionRabbit
{

    public static IServiceCollection AddRabbit(this IServiceCollection services, IConfiguration conf)
    {
        services.Configure<RabbitConf>(conf.GetSection("RabbitConf"));
        services.AddSingleton<RabbitChannelHelper>();
        services.AddSingleton<RabbitHelper>();

        return services;
    }

}