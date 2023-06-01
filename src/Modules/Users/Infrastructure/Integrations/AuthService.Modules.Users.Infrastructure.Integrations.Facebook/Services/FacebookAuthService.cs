using AuthService.Modules.Application.Authentication;
using Facebook;
using AuthService.Modules.Core.ValueObjects;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using AuthService.Modules.Infrastructure.Integrations.Facebook.Contract;
using AuthService.Modules.Application.DTO;
using AuthService.Shared.Storage;
using AuthService.Modules.Users.Infrastructure.Integrations.Facebook.Configuration;

namespace AuthService.Modules.Infrastructure.Integrations.Facebook.Services
{
    internal class FacebookAuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfigurationFacebook _configurationFacebook;
        private readonly FacebookClient fb;
        private readonly string _appId;
        private readonly string _appSecret;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRequestStorage _requestStorage;
        private const string _redirectUrl = "http://localhost:7151/signin-callback";

        public FacebookAuthService(IHttpClientFactory factory, IHttpContextAccessor httpContextAccessor, IRequestStorage requestStorage, IConfigurationFacebook configurationFacebook)
        {
            _httpClient = factory.CreateClient($"{AvailableProviders.Facebook.Name}");

            _requestStorage = requestStorage;

            _configurationFacebook = configurationFacebook;

            _appId = _configurationFacebook.AppId;

            _appSecret = _configurationFacebook.AppSecret;

            _httpContextAccessor = httpContextAccessor;

            fb = new FacebookClient()
            {
                AppId = _appId,
                AppSecret = _appSecret,
            };
        }
        public string Name => "Facebook";

        public string GetAuthorizationUrl()
        {
            _requestStorage.SetSession("Provider", Name);

            var loginUrl = fb.GetLoginUrl(new
            {
                redirect_uri = _redirectUrl,
                scope = "email, public_profile",
                response_type = "code",
            });

            return loginUrl.AbsoluteUri;
        }

        public async Task<UserInfoResult> GetUserInfoAsync()
        {
            string code = _httpContextAccessor.HttpContext.Request.Query["code"];

            dynamic response = await fb.GetTaskAsync("oauth/access_token", new
            {
                client_id = _appId,
                client_secret = _appSecret,
                redirect_uri = _redirectUrl,
                code = code
            });

            fb.AccessToken = response.access_token;

            var jsonData = await fb.GetTaskAsync("me", new { fields = "id,first_name,last_name,email" });

            var user = JsonConvert.DeserializeObject<FacebookUserInfoResult>(jsonData.ToString());

            return new UserInfoResult(user.Email, user.FirstName, user.LastName);

        }
    }
}