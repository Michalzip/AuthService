using AuthService.Modules.Application.Authentication;
using AuthService.Modules.Core.ValueObjects;

namespace AuthService.Modules.Application.Account.Commands.SignWithProvider
{
    internal sealed class SignInWithProviderHandler : IRequestHandler<SignInWithProvider, string>
    {
        private readonly IAuthServiceFactory _authServiceFactory;
        public SignInWithProviderHandler(IAuthServiceFactory authServiceFactory)
        {
            _authServiceFactory = authServiceFactory;
        }

        async Task<string> IRequestHandler<SignInWithProvider, string>.Handle(SignInWithProvider request, CancellationToken cancellationToken)
        {
            //get provider 
            var provider = AvailableProviders.GetProvider(request.Provider);

            var service = _authServiceFactory.Create(provider);

            return service.GetAuthorizationUrl();
        }
    }
}
