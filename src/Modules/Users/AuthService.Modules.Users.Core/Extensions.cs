
using System.Reflection;
using System.Runtime.CompilerServices;
using AuthService.Modules.Core.Services;

[assembly: InternalsVisibleTo("AuthService.Modules.Users.Api")]

namespace AuthService.Modules.Core
{
    internal static class Extensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services
              .AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            services.AddScoped<IUsersService, UsersService>();

            return services;
        }
    }
}