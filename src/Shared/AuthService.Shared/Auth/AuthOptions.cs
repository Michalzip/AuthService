

namespace AuthService.Shared.Auth
{
    public class AuthOptions
    {
        public string Issuer { get; set; }
        public string IssuerSigningKey { get; set; }
        public string Authority { get; set; }
        public string Audience { get; set; }
        public bool SaveToken { get; set; } = true;
        public bool SaveSigninToken { get; set; }
        public bool RequireAudience { get; set; } = true;
        public bool RequireHttpsMetadata { get; set; } = true;
        public string ValidAudience { get; set; }
        public IEnumerable<string> ValidAudiences { get; set; }
        public string ValidIssuer { get; set; }
        public IEnumerable<string> ValidIssuers { get; set; }
        public bool ValidateActor { get; set; }
        public bool ValidateAudience { get; set; } = true;
        public bool ValidateIssuer { get; set; } = true;
        public bool ValidateLifetime { get; set; } = true;
        public bool ValidateTokenReplay { get; set; }
        public bool ValidateIssuerSigningKey { get; set; }

        public CookieOptions Cookie { get; set; }

        public class CookieOptions
        {
            public bool HttpOnly { get; set; }
            public bool Secure { get; set; }
            public string SameSite { get; set; }
        }

    }
}