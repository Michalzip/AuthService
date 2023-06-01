using Ardalis.GuardClauses;
using Microsoft.Extensions.Configuration;

namespace AuthService.Modules.Infrastructure.Integrations.Google.Configuration
{
    public class ConfigurationGoogle : IConfigurationGoogle
    {
        private const string SectionName = "google";
        private readonly IConfigurationSection _configuration;

        public ConfigurationGoogle(IConfiguration configuration)
        {
            _configuration = configuration.GetSection(SectionName);
        }


        //check if key is null or empty and The first argument is the value retrieved from the configuration for example if the  ("ClientSecret"), and the second argument is the name of the value ("ClientSecret") as a string. 
        public string ClientId => Guard.Against.NullOrEmpty(_configuration.GetValue<string>(nameof(ClientId)), nameof(ClientId));
        public string ClientSecret => Guard.Against.NullOrEmpty(_configuration.GetValue<string>(nameof(ClientSecret)), nameof(ClientSecret));
    }
}