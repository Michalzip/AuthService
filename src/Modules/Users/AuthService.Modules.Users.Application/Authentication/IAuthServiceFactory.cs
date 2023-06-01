using AuthService.Modules.Core.ValueObjects;

namespace AuthService.Modules.Application.Authentication
{
    public interface IAuthServiceFactory
    {
        IAuthService Create(Provider provider);
    }
}