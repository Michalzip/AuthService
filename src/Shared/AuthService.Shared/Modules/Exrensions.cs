using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

namespace AuthService.Shared.Modules
{
    public static class Exrensions
    {
        public static IServiceCollection AddModuleInfo(this IServiceCollection services, IList<IModule> modules)
        {
            var moduleInfoProvider = new ModuleInfoProvider();

            var moduleInfo = modules?.Select(x => new ModuleInfo(x.Name)) ?? Enumerable.Empty<ModuleInfo>();

            moduleInfoProvider.Modules.AddRange(moduleInfo);

            services.AddSingleton(moduleInfoProvider);

            return services;
        }

        public static void MapModuleInfo(this IEndpointRouteBuilder endpoint)
        {
            endpoint.MapGet("/", context =>
            {
                var moduleInfoProvider = context.RequestServices.GetRequiredService<ModuleInfoProvider>();
                return context.Response.WriteAsJsonAsync(moduleInfoProvider.Modules);
            });
        }

        public static IHostBuilder ConfigureModules(this IHostBuilder builder)
        {
            return builder.ConfigureAppConfiguration((ctx, cfg) =>
              {
                  foreach (var settings in GetSettings("*")) //get all json settings
                  {
                      cfg.AddJsonFile(settings);
                  }

                  foreach (var settings in
                           GetSettings($"*.{ctx.HostingEnvironment.EnvironmentName}")) //devide to develpment/production
                  {
                      cfg.AddJsonFile(settings);
                  }

                  IEnumerable<string> GetSettings(string pattern)
                      => Directory.EnumerateFiles(ctx.HostingEnvironment.ContentRootPath,
                          $"module.{pattern}.json", SearchOption.AllDirectories);
              });
        }
    }
}