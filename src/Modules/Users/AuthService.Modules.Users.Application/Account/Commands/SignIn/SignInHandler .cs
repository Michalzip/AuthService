
using AuthService.Modules.Core.Entities;
using AuthService.Modules.Core.Repositories;
using AuthService.Modules.Core.Services;
using AuthService.Modules.Users.Core.Entities;
using AuthService.Modules.Users.Core.Exceptions;
using AuthService.Shared.Auth;
using Microsoft.AspNetCore.Identity;

namespace AuthService.Modules.Users.Application.Account.Commands.SignIn
{
    internal sealed class SignInHandler : IRequestHandler<SignIn>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserRequestStorage _userRequestStorage;
        private readonly IAuthManager _authManager;
        private readonly IPasswordHasher<User> _passwordHasher;

        public SignInHandler(IUserRepository userRepository, IUserRequestStorage userRequestStorage, IAuthManager authManager, IPasswordHasher<User> passwordHasher)
        {
            _userRepository = userRepository;
            _userRequestStorage = userRequestStorage;
            _authManager = authManager;
            _passwordHasher = passwordHasher;
        }
        public async Task Handle(SignIn request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(request.Email.ToLowerInvariant());

            if (user == null) throw new InvalidCredentialsException();

            if (user.State != UserState.Active)
            {
                throw new UserNotActiveException(user.Id);
            }

            if (_passwordHasher.VerifyHashedPassword(default, user.Password, request.Password) ==
           PasswordVerificationResult.Failed)
            {
                throw new InvalidCredentialsException();
            }

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