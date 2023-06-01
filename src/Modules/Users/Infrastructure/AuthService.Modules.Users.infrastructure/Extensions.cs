
using System.Runtime.CompilerServices;
using AuthService.Modules.Infrastructure.Integrations.Google;
using AuthService.Modules.Infrastructure.Integrations.Facebook;
using AuthService.Modules.Application.Authentication;
using AuthService.Modules.infrastructure.Authentication;
using AuthService.Modules.Core.Services;
using AuthService.Shared.Sql;
using AuthService.Modules.infrastructure.EF.Context;
using AuthService.Modules.Core.Repositories;
using AuthService.Modules.infrastructure.Repositories;
using AuthService.Shared;
using AuthService.Modules.infrastructure.EF;
using AuthService.Modules.Users.infrastructure.EF;
using AuthService.Modules.Core.Entities;
using Microsoft.AspNetCore.Identity;


[assembly: InternalsVisibleTo("AuthService.Modules.Users.Api")]
namespace AuthService.Modules.infrastructure
{
    internal static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IUserRequestStorage, UserRequestStorage>();
            services.AddGoogle();
            services.AddFacebook();
            services.AddSql<UsersDbContext>();
            services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthServiceFactory, AuthServiceFactory>();
            services.AddInitializer<UsersInitializer>();
            services.AddInitializer<AdminInitializer>();

            return services;
        }
    }
}