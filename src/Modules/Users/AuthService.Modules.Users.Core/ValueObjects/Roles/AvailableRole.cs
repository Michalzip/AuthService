
namespace AuthService.Modules.Core.ValueObjects.Roles
{
    public static class AvailableRole
    {
        public const string Admin = nameof(Admin);

        public const string User = nameof(User);

        internal static readonly IReadOnlyCollection<string> AllCodes = new List<string>
        {
             Admin, User
        };
    }
}