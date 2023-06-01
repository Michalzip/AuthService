using AuthService.Modules.Application.Authentication;
using AuthService.Modules.Core.ValueObjects;
using AuthService.Modules.Core.ValueObjects.Exceptions;

namespace AuthService.Modules.infrastructure.Authentication
{
    internal class AuthServiceFactory : IAuthServiceFactory
    {
        // get all IAuthServices
        private readonly IEnumerable<IAuthService> _authServices;

        public AuthServiceFactory(IEnumerable<IAuthService> authServices)
        {
            _authServices = authServices;
        }

        public IAuthService Create(Provider provider)
        {
            var authServices = _authServices.SingleOrDefault(q => q.Name == provider.Name);
            //??
            if (authServices == null) throw new UnsupportedProviderException($"{provider.Name}");

            return authServices;
        }
    }

}