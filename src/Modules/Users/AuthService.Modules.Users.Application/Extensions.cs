using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("AuthService.Modules.Users.Api")]

namespace AuthService.Modules.Application
{
    internal static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            return services;
        }
    }
}


