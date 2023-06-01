
using AuthService.Modules.Users.Application.Account.Commands.SignUp;

namespace AuthService.Modules.Users.Api.Endpoints.Account.Commands.Signup
{
    internal sealed class SignUpEndpoint : EndpointBaseAsync
    .WithRequest<SignUp>
    .WithoutResult
    {
        private readonly IMediator _mediator;

        public SignUpEndpoint(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("sign-up")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status400BadRequest)]
        public override async Task<ActionResult> HandleAsync(SignUp request, CancellationToken cancellationToken = default)
        {
            var userSignInId = await _mediator.Send(request);

            return Ok(userSignInId.UserId + " success sign up");
        }
    }
}