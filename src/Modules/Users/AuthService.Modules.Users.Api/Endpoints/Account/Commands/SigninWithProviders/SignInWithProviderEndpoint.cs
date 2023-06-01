using Swashbuckle.AspNetCore.Annotations;
using AuthService.Modules.Application.Account.Commands.SignWithProvider;

namespace AuthService.Modules.Api.Endpoints.Account.Commands.SigninWithProviders
{
    internal sealed class SignInWithProviderEndpoint : EndpointBaseAsync
    .WithRequest<SignInWithProvider>
    .WithoutResult
    {
        private readonly IMediator _mediator;

        public SignInWithProviderEndpoint(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("sign-in/providers")]
        [SwaggerOperation("Sign In With Providers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status400BadRequest)]
        public override async Task<string> HandleAsync([FromBody] SignInWithProvider request, CancellationToken cancellationToken = default)
        {
            return await _mediator.Send(request, cancellationToken);
        }
    }
}