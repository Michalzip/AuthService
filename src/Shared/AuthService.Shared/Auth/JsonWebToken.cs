
namespace AuthService.Shared.Auth
{
    public class JsonWebToken
    {
        public string AccessToken { get; set; }
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public IDictionary<string, IEnumerable<string>> Claims { get; set; }
    }
}