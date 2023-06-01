
using AuthService.Modules.Core.ValueObjects.Roles;

namespace AuthService.Modules.Core.Entities
{
    public class Role
    {
        public string Name { get; set; }
        public IEnumerable<string> Permissions { get; set; }
        public IEnumerable<User> Users { get; set; }

        public static string Default => User;
        public const string Admin = $"{AvailableRole.Admin}";
        public const string User = $"{AvailableRole.User}";
    }
}