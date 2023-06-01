
namespace AuthService.Modules.Infrastructure.Integrations.Google.Configuration
{
    public interface IConfigurationGoogle
    {
        string ClientId { get; }
        string ClientSecret { get; }
    }
}