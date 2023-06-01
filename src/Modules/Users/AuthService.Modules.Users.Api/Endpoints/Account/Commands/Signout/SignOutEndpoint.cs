using AuthService.Modules.Api.Endpoints;
using AuthService.Modules.Application.Account.Commands.SignOut;

namespace AuthService.Modules.Api.Endpoints.Account.Commands.Signout
{
    internal sealed class SignOutEndpoint : EndpointBaseAsync
     .WithoutRequest
     .WithoutResult
    {
        private readonly IMediator _mediator;
        private readonly CookieOptions _cookieOptions;

        public SignOutEndpoint(IMediator mediator, CookieOptions cookieOptions)
        {
            _mediator = mediator;
            _cookieOptions = cookieOptions;
        }

        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpDelete("sign-out")]
        public override async Task<IActionResult> HandleAsync(CancellationToken cancellationToken = default)
        {
            await _mediator.Send(new SignOut(User.GetUserId()));
            DeleteCookie(AccountsEndpoint.AccessTokenCookie);

            return NoContent();
        }

        private void DeleteCookie(string key) => Response.Cookies.Delete(key, _cookieOptions);
    }
}