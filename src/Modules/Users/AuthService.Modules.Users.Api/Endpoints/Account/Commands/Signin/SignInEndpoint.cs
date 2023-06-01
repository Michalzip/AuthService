
using AuthService.Modules.Api.Endpoints;
using AuthService.Modules.Core.Services;
using AuthService.Modules.Users.Application.Account.Commands.SignIn;

namespace AuthService.Modules.Users.Api.Endpoints.Account.Commands.Signin
{
    internal sealed class SignInEndpoint : EndpointBaseAsync
    .WithRequest<SignIn>
    .WithoutResult
    {
        private readonly IMediator _mediator;
        private readonly CookieOptions _cookieOptions;

        private readonly IUserRequestStorage _userRequestStorage;
        public SignInEndpoint(IMediator mediator, CookieOptions cookieOptions, IUserRequestStorage userRequestStorage)
        {
            _mediator = mediator;
            _cookieOptions = cookieOptions;
            _userRequestStorage = userRequestStorage;
        }

        [HttpPost("sign-in")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status400BadRequest)]
        public override async Task<ActionResult> HandleAsync(SignIn request, CancellationToken cancellationToken = default)
        {

            await _mediator.Send(request, cancellationToken);

            var jwt = _userRequestStorage.GetToken(request.Id);

            AddCookie(AccountsEndpoint.AccessTokenCookie, jwt.AccessToken);

            return Ok("User Successfully authenticated");
        }

        private void AddCookie(string key, string value) => Response.Cookies.Append(key, value, _cookieOptions);
    }
}