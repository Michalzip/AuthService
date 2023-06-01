using AuthService.Modules.Core.Services;
using AuthService.Modules.Users.Core.Exceptions;

namespace AuthService.Modules.Users.Application.Account.Commands.SignUp
{
    internal sealed class SignUpHandler : IRequestHandler<SignUp, SignUpResponse>
    {
        private readonly IUsersService _usersService;

        public SignUpHandler(IUsersService usersService)
        {
            _usersService = usersService;
        }
        public async Task<SignUpResponse> Handle(SignUp request, CancellationToken cancellationToken)
        {
            if (request.Password != request.ConfirmPassword) throw new PasswordNotMatchException();

            var user = await _usersService.CreateUserToConfirm(request.Email, request.FirstName, request.LastName, request.Password,
                           cancellationToken: cancellationToken);

            return new SignUpResponse(user.Id);
        }
    }
}