using AuthService.Modules.Core.Repositories;
using AuthService.Modules.Application.Account.Queries.GetAccount.DTO;

namespace AuthService.Modules.Application.Account.Queries.GetAccount
{
    public class GetAccountHandler : IRequestHandler<GetAccount, AccountDto>
    {
        private readonly IUserRepository _userRepository;
        public GetAccountHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        async Task<AccountDto> IRequestHandler<GetAccount, AccountDto>.Handle(GetAccount request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(request.UserId);

            return user.AsAccountDto();
        }
    }
}