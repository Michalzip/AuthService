using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AuthService.Shared.Sql
{
    public static class Extensions
    {
        public static IServiceCollection AddSql<T>(this IServiceCollection services) where T : DbContext
        {
            var options = services.GetOptions<SqlOptions>("sql");

            services.AddDbContext<T>(o => o.UseSqlServer(options.ConnectionString));

            return services;
        }

    }
}