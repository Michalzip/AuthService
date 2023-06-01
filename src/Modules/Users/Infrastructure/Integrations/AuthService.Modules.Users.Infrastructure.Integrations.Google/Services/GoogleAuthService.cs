using AuthService.Modules.Application.Authentication;
using AuthService.Modules.Core.ValueObjects;
using AuthService.Modules.Infrastructure.Integrations.Google.Configuration;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Util.Store;
using Google.Apis.Auth.OAuth2.Responses;
using Microsoft.AspNetCore.Http;
using AuthService.Shared.Storage;
using AuthService.Modules.Application.Exceptions;
using static Google.Apis.Auth.GoogleJsonWebSignature;
using Microsoft.Extensions.Configuration;
using AuthService.Modules.Application.DTO;


namespace AuthService.Modules.Infrastructure.Integrations.Google.Services
{
    internal class GoogleAuthService : IAuthService
    {
        private readonly IConfigurationGoogle _configurationGoogle;
        private readonly HttpClient _httpClient;
        private readonly IRequestStorage _requestStorage;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly GoogleAuthorizationCodeFlow _flow;
        private const string _redirectUrl = "http://localhost:7151/signin-callback";



        public GoogleAuthService(IConfigurationGoogle configurationGoogle, IHttpClientFactory factory, IHttpContextAccessor httpContextAccessor, IRequestStorage requestStorage)
        {
            _configurationGoogle = configurationGoogle;

            _requestStorage = requestStorage;

            _httpContextAccessor = httpContextAccessor;

            _httpClient = factory.CreateClient($"{AvailableProviders.Google.Name}");



            _flow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = new ClientSecrets
                {
                    ClientId = _configurationGoogle.ClientId,
                    ClientSecret = _configurationGoogle.ClientSecret
                },
                Scopes = new[] { "profile", "email" },

                DataStore = new FileDataStore("Drive.Api.Auth.Store"),
            });
        }

        public string Name => "Google";

        public string GetAuthorizationUrl()
        {
            //set session about provider   
            _requestStorage.SetSession("Provider", Name);
            return _flow.CreateAuthorizationCodeRequest(_redirectUrl).Build().ToString();
        }

        public async Task<UserInfoResult> GetUserInfoAsync()
        {
            string code = _httpContextAccessor.HttpContext.Request.Query["code"];

            TokenResponse tokenResponse = await _flow.ExchangeCodeForTokenAsync("email", code, _redirectUrl, CancellationToken.None);

            var tokenPayload = await ValidateAsync(tokenResponse.IdToken);

            return new UserInfoResult(tokenPayload.Email, tokenPayload.GivenName, tokenPayload.FamilyName);
        }
    }
}