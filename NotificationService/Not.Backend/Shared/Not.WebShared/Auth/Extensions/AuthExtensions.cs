

using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication;
using Not.WebShared.Auth;

namespace Not.WebShared.Auth.Extensions;

public static class AuthExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddSimpleAuth()
        {
            services.AddAuthentication("Simple")
                .AddScheme<AuthenticationSchemeOptions, SimpleAuthHandler>("Simple", opts => { });

            return services;
        }
    }
}