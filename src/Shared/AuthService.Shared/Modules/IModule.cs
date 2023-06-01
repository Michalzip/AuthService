
using Microsoft.Extensions.DependencyInjection;

namespace AuthService.Shared.Modules
{
    public interface IModule
    {
        string Name { get; }
        IEnumerable<string> Policies => null;
        void Register(IServiceCollection services);
    }
}