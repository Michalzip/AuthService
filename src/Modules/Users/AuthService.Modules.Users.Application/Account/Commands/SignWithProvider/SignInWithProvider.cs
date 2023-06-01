namespace AuthService.Modules.Application.Account.Commands.SignWithProvider
{
    internal record SignInWithProvider(string Provider) : IRequest<string>;
}