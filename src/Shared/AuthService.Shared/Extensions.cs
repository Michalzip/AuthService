using AuthService.Shared.Api;
using AuthService.Shared.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using AuthService.Shared.Storage;
using Microsoft.Extensions.Configuration;
using AuthService.Shared.Auth;
using AuthService.Shared.Clock;
using AuthService.Shared.Services;

namespace AuthService.Shared
{
    public static class Extensions
    {
        public static IServiceCollection AddShared(this IServiceCollection services, IList<IModule> modules, IConfiguration configuration)
        {
            services.AddCors();

            services.AddMemoryCache();
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(12);//We set Time here 
                options.Cookie.Name = "MySession";
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.Cookie.IsEssential = true;
            });

            services.AddScoped<CookieOptions>();

            services.AddSingleton<IClock, UtcClock>();

            services.AddSingleton<IRequestStorage, RequestStorage>();

            services.AddHttpContextAccessor();

            services.AddAuth(modules);

            services.AddSwaggerGen();

            services.AddControllers().ConfigureApplicationPartManager(manager => manager.FeatureProviders.Add(new InternalControllerFeatureProvider()));

            services.AddHostedService<AppInitializer>();

            services.AddErrorHandling();

            services.AddModuleInfo(modules);

            return services;
        }

        public static IApplicationBuilder UseShared(this IApplicationBuilder app)
        {
            app.UseErrorHandling();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseCors();

            app.UseRouting();

            app.UseAuth();
            app.UseCookiePolicy();
            app.UseSession();

            app.UseAuthorization();

            return app;
        }

        public static IServiceCollection AddInitializer<T>(this IServiceCollection services) where T : class, IInitializer
              => services.AddTransient<IInitializer, T>();
        public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : new()
        {
            var options = new T();
            configuration.GetSection(sectionName).Bind(options);
            return options;
        }

        //get json propeties
        public static T GetOptions<T>(this IServiceCollection services, string sectionName) where T : new()
        {
            using var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            return configuration.GetOptions<T>(sectionName);
        }
    }
}