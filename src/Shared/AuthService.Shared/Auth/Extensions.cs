using System.Text;
using AuthService.Shared;
using AuthService.Shared.Modules;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.Shared.Auth
{
    public static class Extensions
    {
        private const string AccessTokenCookieName = "__access-token";
        private const string AuthorizationHeader = "authorization";
        public static IServiceCollection AddAuth(this IServiceCollection services, IList<IModule> modules = null)
        {
            var authOptions = services.GetOptions<AuthOptions>("auth");

            services.AddScoped<IAuthManager, AuthManager>();

            services
               .AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
                 {
                     options.Authority = authOptions.Authority;
                     options.Audience = authOptions.Audience;
                     options.RequireHttpsMetadata = authOptions.RequireHttpsMetadata;
                     options.SaveToken = authOptions.SaveToken;
                     options.TokenValidationParameters = new TokenValidationParameters
                     {
                         RequireAudience = authOptions.RequireAudience,
                         ValidIssuer = authOptions.ValidIssuer,
                         ValidIssuers = authOptions.ValidIssuers,
                         ValidateActor = authOptions.ValidateActor,
                         ValidAudience = authOptions.ValidAudience,
                         ValidAudiences = authOptions.ValidAudiences,
                         ValidateAudience = authOptions.ValidateAudience,
                         ValidateIssuer = authOptions.ValidateIssuer,
                         ValidateLifetime = authOptions.ValidateLifetime,
                         ValidateTokenReplay = authOptions.ValidateTokenReplay,
                         ValidateIssuerSigningKey = authOptions.ValidateIssuerSigningKey,
                         SaveSigninToken = authOptions.SaveSigninToken,
                         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authOptions.IssuerSigningKey)),
                         ClockSkew = TimeSpan.Zero
                     };
                     options.Events = new JwtBearerEvents
                     {
                         //identity user from cookie instead header
                         OnMessageReceived = context =>
                         {
                             if (context.Request.Cookies.TryGetValue(AccessTokenCookieName, out var token))
                             {
                                 context.Token = token;
                             }
                             return Task.CompletedTask;
                         }
                     };
                 });

            if (string.IsNullOrWhiteSpace(authOptions.IssuerSigningKey))
            {
                throw new ArgumentException("Missing issuer signing key.", nameof(authOptions.IssuerSigningKey));
            }

            services.AddSingleton(authOptions);
            services.AddSingleton(authOptions.Cookie);

            var policies = modules?.SelectMany(x => x.Policies ?? Enumerable.Empty<string>()) ??
                                   Enumerable.Empty<string>();

            services.AddAuthorization(authorization =>
        {
            foreach (var policy in policies)
            {
                authorization.AddPolicy(policy, x => x.RequireClaim("permissions", policy));
            }
        });

            return services;
        }

        public static IApplicationBuilder UseAuth(this IApplicationBuilder app)
        {
            app.UseAuthentication();

            app.Use(async (ctx, next) =>
                   {
                       if (ctx.Request.Headers.ContainsKey(AuthorizationHeader))
                       {
                           ctx.Request.Headers.Remove(AuthorizationHeader);
                       }

                       if (ctx.Request.Cookies.ContainsKey(AccessTokenCookieName))
                       {
                           var authenticateResult = await ctx.AuthenticateAsync(JwtBearerDefaults.AuthenticationScheme);
                           if (authenticateResult.Succeeded && authenticateResult.Principal is not null)
                           {
                               ctx.User = authenticateResult.Principal;
                           }
                       }

                       await next();
                   });

            return app;
        }
    }
}
