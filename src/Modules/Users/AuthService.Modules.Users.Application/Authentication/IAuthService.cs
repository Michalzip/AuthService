using AuthService.Modules.Application.DTO;

namespace AuthService.Modules.Application.Authentication
{
    public interface IAuthService
    {
        string Name { get; }

        string GetAuthorizationUrl();

        Task<UserInfoResult> GetUserInfoAsync();
    }
}