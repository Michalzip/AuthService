using AuthService.Modules.Application.Account.Commands.SignWithProvider.Callback;
using AuthService.Modules.Core.Services;
using Swashbuckle.AspNetCore.Annotations;
using AuthService.Modules.Api.Endpoints;


namespace AuthService.Modules.Api.Endpoints.Account.Commands.SigninWithProviders
{
    internal sealed class SignInWithProviderCallbackEndpoint : EndpointBaseAsync
        .WithoutRequest
         .WithoutResult
    {
        private readonly IMediator _mediator;
        private readonly CookieOptions _cookieOptions;
        private readonly IUserRequestStorage _userRequestStorage;
        public SignInWithProviderCallbackEndpoint(IMediator mediator, CookieOptions cookieOptions, IUserRequestStorage userRequestStorage)
        {
            _mediator = mediator;
            _cookieOptions = cookieOptions;
            _userRequestStorage = userRequestStorage;
        }

        [HttpGet("signin-callback")]
        [SwaggerOperation("callback provider")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status400BadRequest)]

        public override async Task<ActionResult> HandleAsync(CancellationToken cancellationToken = default)
        {
            var guid = GuidGenerator.GenerateGuid();

            await _mediator.Send(new SignInWithProviderCallback(guid), cancellationToken);

            var jwt = _userRequestStorage.GetToken(guid);

            AddCookie(AccountsEndpoint.AccessTokenCookie, jwt.AccessToken);

            return Ok("User Successfully authenticated");
        }

        private void AddCookie(string key, string value) => Response.Cookies.Append(key, value, _cookieOptions);
    }
}
