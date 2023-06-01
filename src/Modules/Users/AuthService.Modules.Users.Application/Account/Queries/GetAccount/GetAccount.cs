
using AuthService.Modules.Application.Account.Queries.GetAccount.DTO;

namespace AuthService.Modules.Application.Account.Queries.GetAccount
{
    internal record GetAccount(Guid UserId) : IRequest<AccountDto>;
}