

namespace AuthService.Modules.Users.Infrastructure.Integrations.Facebook.Configuration
{
    public interface IConfigurationFacebook
    {
        string AppId { get; }
        string AppSecret { get; }
    }
}