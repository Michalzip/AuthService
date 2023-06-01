using System.ComponentModel.DataAnnotations;

namespace AuthService.Modules.Users.Application.Account.Commands.SignUp
{
    public record SignUp([Required][EmailAddress] string Email, [Required] string FirstName, [Required] string LastName, [Required] string Password, [Required] string ConfirmPassword) : IRequest<SignUpResponse>;
}