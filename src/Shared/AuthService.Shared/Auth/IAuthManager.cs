
namespace AuthService.Shared.Auth
{
    public interface IAuthManager
    {
        JsonWebToken CreateToken(Guid userId, string role = null, IDictionary<string, IEnumerable<string>> claims = null);
    }
}