using System.Reflection;
using AuthService.Modules.Application;
using AuthService.Modules.Core;
using AuthService.Modules.infrastructure;
using AuthService.Shared.Modules;
namespace AuthService.Modules.Api
{
    internal class UserModule : IModule
    {

        public string Name { get; } = "main";

        public void Register(IServiceCollection services)
        {
            services.AddApplication();
            services.AddCore();
            services.AddInfrastructure();

        }
    }
}