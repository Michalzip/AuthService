
using Microsoft.Extensions.DependencyInjection;
using AuthService.Shared.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace AuthService.Shared.Exceptions
{
    internal static class Extensions
    {
        public static IServiceCollection AddErrorHandling(this IServiceCollection services) =>
            services
                .AddScoped<ErrorHandlerMiddleware>();

        public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder services)
        {
            return services.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}