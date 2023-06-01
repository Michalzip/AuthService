

using AuthService.Modules.Core.Entities;
using AuthService.Modules.Core.Exceptions;
using AuthService.Modules.Core.Repositories;
using AuthService.Modules.Core.ValueObjects;
using AuthService.Modules.Core.ValueObjects.Roles;
using AuthService.Modules.Users.Core.Entities;
using AuthService.Shared.Clock;
using AuthService.Shared.ValueObjects.Password;
using Microsoft.AspNetCore.Identity;

namespace AuthService.Modules.Core.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IClock _clock;
        private const string RoleUser = AvailableRole.User;
        public UsersService(IUserRepository userRepository, IRoleRepository roleRepository, IPasswordHasher<User> passwordHasher, IClock clock)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _passwordHasher = passwordHasher;
            _clock = clock;
        }

        //? create user
        public async Task<User> CreateUserToConfirm(string email, string firstName, string lastName, string password, CancellationToken cancellationToken)
        {

            var user = await _userRepository.GetAsync(email);

            if (user is not null)
                throw new EmailInUseException();

            var role = await _roleRepository.GetAsync(RoleUser);

            var now = _clock.CurrentDateTime();

            var hashPassword = _passwordHasher.HashPassword(default, password);

            user = User.Create(email, firstName, lastName, hashPassword, role, UserState.Active, now);

            await _userRepository.AddAsync(user);

            return user;

        }

        //create user with providers
        public async Task<User> CreateUser(string email, string firstName, string lastName, Provider provider, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.GetAsync(email);

            if (user is not null)
                throw new EmailInUseException();

            var now = _clock.CurrentDateTime();

            var role = await _roleRepository.GetAsync(RoleUser);

            var randomPassword = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

            var hashPassword = _passwordHasher.HashPassword(default, randomPassword);

            user = User.CreateWithProvider(email, firstName, lastName, hashPassword, role, UserState.Active, now, provider);

            await _userRepository.AddAsync(user);

            return user;
        }
    }
}