using AuthService.Modules.Application.Authentication;
using AuthService.Modules.Application.Exceptions;
using AuthService.Modules.Core.Exceptions;
using AuthService.Modules.Core.Repositories;
using AuthService.Modules.Core.Services;
using AuthService.Modules.Core.ValueObjects;
using AuthService.Shared.Auth;
using AuthService.Shared.Storage;

namespace AuthService.Modules.Application.Account.Commands.SignWithProvider.Callback
{
    public class SignInWithProviderCallbackHandler : IRequestHandler<SignInWithProviderCallback>
    {
        private readonly IRequestStorage _requestStorage;
        private readonly IAuthServiceFactory _authServiceFactory;
        private readonly IUserRequestStorage _userRequestStorage;
        private readonly IAuthManager _authManager;
        private readonly IUserRepository _userRepository;
        private readonly IUsersService _usersService;
        public SignInWithProviderCallbackHandler(IRequestStorage requestStorage, IAuthServiceFactory authServiceFactory, IUserRequestStorage userRequestStorage, IAuthManager authManager, IUserRepository userRepository, IUsersService usersService)
        {
            _requestStorage = requestStorage;
            _authServiceFactory = authServiceFactory;
            _userRequestStorage = userRequestStorage;
            _authManager = authManager;
            _userRepository = userRepository;
            _usersService = usersService;
        }
        async Task IRequestHandler<SignInWithProviderCallback>.Handle(SignInWithProviderCallback request, CancellationToken cancellationToken)
        {

            var providerName = _requestStorage.GetSession("Provider");

            if (providerName == null) throw new SessionExpiresException();

            var provider = AvailableProviders.GetProvider(providerName);

            //get facebook or google serivce
            var service = _authServiceFactory.Create(provider);

            var userinfo = await service.GetUserInfoAsync();
            //try sign in if user is null add him to db 
            var user = await _userRepository.GetAsync(userinfo.Email.ToLowerInvariant());

            if (user is null) user = await _usersService.CreateUser(userinfo.Email, userinfo.FirstName, userinfo.LastName, provider, cancellationToken: cancellationToken);

            var claims = new Dictionary<string, IEnumerable<string>>
            {
                ["permissions"] = user.Role.Permissions
            };

            var jwt = _authManager.CreateToken(user.Id, user.Role.Name, claims: claims);

            jwt.Email = user.Email;

            _userRequestStorage.SetToken(request.Id, jwt);

        }

    }
}