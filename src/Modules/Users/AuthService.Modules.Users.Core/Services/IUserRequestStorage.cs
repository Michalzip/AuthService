using AuthService.Shared.Auth;

namespace AuthService.Modules.Core.Services
{
    public interface IUserRequestStorage
    {
        void SetToken(Guid commandId, JsonWebToken jwt);
        JsonWebToken GetToken(Guid commandId);
    }
}