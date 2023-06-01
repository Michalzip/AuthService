using Microsoft.Extensions.Logging;

namespace AuthService.Modules.Application.Account.Commands.SignOut
{
    public class SignOutHandler : IRequestHandler<SignOut>
    {
        private readonly ILogger<SignOutHandler> _logger;

        public SignOutHandler(ILogger<SignOutHandler> logger)
        {
            _logger = logger;
        }
        async Task IRequestHandler<SignOut>.Handle(SignOut request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            _logger.LogInformation($"User with ID: '{request.UserId}' has signed out.");
        }
    }
}