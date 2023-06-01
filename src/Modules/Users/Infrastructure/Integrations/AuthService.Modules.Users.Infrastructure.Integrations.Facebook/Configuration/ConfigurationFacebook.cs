

using Ardalis.GuardClauses;
using Microsoft.Extensions.Configuration;

namespace AuthService.Modules.Users.Infrastructure.Integrations.Facebook.Configuration
{
    public class ConfigurationFacebook : IConfigurationFacebook
    {
        private const string SectionName = "facebook";

        private readonly IConfigurationSection _configuration;

        public ConfigurationFacebook(IConfiguration configuration)
        {
            _configuration = configuration.GetSection(SectionName);
        }
        public string AppId => Guard.Against.NullOrEmpty(_configuration.GetValue<string>(nameof(AppId)), nameof(AppId));
        public string AppSecret => Guard.Against.NullOrEmpty(_configuration.GetValue<string>(nameof(AppSecret)), nameof(AppSecret));
    }
}