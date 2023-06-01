
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.Shared.Auth
{
    public sealed class AuthManager : IAuthManager
    {
        private readonly SigningCredentials _signingCredentials;
        private static readonly Dictionary<string, IEnumerable<string>> EmptyClaims = new();
        private readonly AuthOptions _options;
        private readonly string _issuer;

        public AuthManager(AuthOptions options)
        {
            var issuerSigningKey = options.IssuerSigningKey;
            if (issuerSigningKey is null)
            {
                throw new InvalidOperationException("Issuer signing key not set.");
            }

            _options = options;

            _signingCredentials =
            new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.IssuerSigningKey)),
                SecurityAlgorithms.HmacSha256);

            _issuer = options.Issuer;
        }

        public JsonWebToken CreateToken(Guid userId, string role = null, IDictionary<string, IEnumerable<string>> claims = null)

        {
            var jwtClaims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

            if (!string.IsNullOrWhiteSpace(role))
            {
                jwtClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            //add my custom claims to jwt token
            if (claims?.Any() is true)
            {
                var customClaims = new List<Claim>();
                foreach (var (claim, values) in claims)
                {
                    customClaims.AddRange(values.Select(value => new Claim(claim, value)));
                }

                jwtClaims.AddRange(customClaims);
            }

            var jwt = new JwtSecurityToken(
        _issuer,
        claims: jwtClaims,
        expires: DateTime.UtcNow.AddHours(1), // Expiration time
        signingCredentials: _signingCredentials);

            string token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new JsonWebToken
            {
                AccessToken = token,
                UserId = userId,
                Role = role ?? string.Empty,
                Claims = claims ?? EmptyClaims
            };
        }

    }
}