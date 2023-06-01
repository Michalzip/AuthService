using AuthService.Modules.Application.Account.Queries.GetAccount.DTO;
using AuthService.Modules.Core.Entities;
using AuthService.Modules.Users.Core.Entities;

namespace AuthService.Modules.Application.Account.Queries.GetAccount
{
    internal static class Extensions
    {
        private static readonly Dictionary<UserState, string> States = new()
        {
            [UserState.Active] = UserState.Active.ToString().ToLowerInvariant(),
            [UserState.Locked] = UserState.Locked.ToString().ToLowerInvariant()
        };

        public static AccountDto AsAccountDto(this User user)
               => new()
               {
                   UserId = user.Id,
                   Email = user.Email,
                   FirstName = user.FirstName,
                   LastName = user.LastName,
                   State = States[user.State],
                   Role = user.Role.Name,
                   CreatedAt = user.CreatedAt,
                   Permissions = user.Role.Permissions
               };
    }
}