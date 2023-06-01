
namespace AuthService.Modules.Application.Account.Commands.SignOut
{
    public record SignOut(Guid UserId) : IRequest;
}