using System.Runtime.CompilerServices;
using AuthService.Modules.Application.Authentication;
using AuthService.Modules.Infrastructure.Integrations.Google.Configuration;
using AuthService.Modules.Infrastructure.Integrations.Google.Services;
using Microsoft.Extensions.DependencyInjection;


[assembly: InternalsVisibleTo("AuthService.Modules.Users.infrastructure")]

namespace AuthService.Modules.Infrastructure.Integrations.Google
{
    internal static class Extensions
    {
        internal static IServiceCollection AddGoogle(this IServiceCollection services)
        {
            services.AddSingleton<IConfigurationGoogle, ConfigurationGoogle>();
            services.AddScoped<IAuthService, GoogleAuthService>();
            services.AddHttpClient();

            return services;
        }
    }
}