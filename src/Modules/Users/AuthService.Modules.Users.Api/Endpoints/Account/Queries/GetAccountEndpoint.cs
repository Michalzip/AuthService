
using AuthService.Modules.Application.Account.Queries.GetAccount;
using AuthService.Modules.Application.Account.Queries.GetAccount.DTO;
using AuthService.Shared.Claims;
using Swashbuckle.AspNetCore.Annotations;

namespace AuthService.Modules.Api.Endpoints.Account.Queries
{
    internal sealed class GetAccountEndpoint : EndpointBaseAsync
        .WithoutRequest
         .WithActionResult<AccountDto>
    {
        private readonly IMediator _mediator;

        public GetAccountEndpoint(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get-account")]
        [Authorize]
        [SwaggerOperation("Get account")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        public override async Task<ActionResult<AccountDto>> HandleAsync(CancellationToken cancellationToken = default)
        {
            var query = new GetAccount(User.GetUserId());

            var response = await _mediator.Send(query);

            if (response is null) return NotFound();

            return response;
        }
    }
}