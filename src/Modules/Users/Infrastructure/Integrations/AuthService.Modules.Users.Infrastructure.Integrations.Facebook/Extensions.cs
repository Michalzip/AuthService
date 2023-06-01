using AuthService.Modules.Application.Authentication;
using Microsoft.Extensions.DependencyInjection;
using AuthService.Modules.Infrastructure.Integrations.Facebook.Services;
using System.Runtime.CompilerServices;
using AuthService.Modules.Users.Infrastructure.Integrations.Facebook.Configuration;

[assembly: InternalsVisibleTo("AuthService.Modules.Users.infrastructure")]
namespace AuthService.Modules.Infrastructure.Integrations.Facebook
{
    internal static class Extensions
    {
        internal static IServiceCollection AddFacebook(this IServiceCollection services)
        {
            services.AddSingleton<IConfigurationFacebook, ConfigurationFacebook>();
            services.AddScoped<IAuthService, FacebookAuthService>();

            return services;
        }
    }
}