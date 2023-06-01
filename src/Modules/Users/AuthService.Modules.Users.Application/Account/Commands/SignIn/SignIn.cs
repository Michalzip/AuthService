using System.ComponentModel.DataAnnotations;

namespace AuthService.Modules.Users.Application.Account.Commands.SignIn
{
    internal record SignIn([Required][EmailAddress] string Email, [Required] string Password) : IRequest
    {
        public Guid Id { get; init; } = Guid.NewGuid();
    }
}