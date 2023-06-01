

using AuthService.Modules.Core.Entities;
using AuthService.Modules.Core.ValueObjects;
using AuthService.Shared.ValueObjects.Password;

namespace AuthService.Modules.Core.Services
{
    public interface IUsersService
    {
        public Task<User> CreateUserToConfirm(string email, string firstname, string lastname, string password, CancellationToken cancellationToken);
        Task<User> CreateUser(string email, string firstName, string lastName, Provider provider, CancellationToken cancellationToken = default);
    }
}