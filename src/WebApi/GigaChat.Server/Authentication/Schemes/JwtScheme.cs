using System.Security.Claims;
using System.Text;

using GigaChat.Contracts.Common.Routes;
using GigaChat.Core.Common.Services.Models;
using GigaChat.Server.Authentication.Constants;

using IdentityModel;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace GigaChat.Server.Authentication.Schemes;

public static class JwtScheme
{
    public const string SchemeName = JwtBearerDefaults.AuthenticationScheme;

    public static void AddJwtScheme(this AuthenticationBuilder builder, IConfiguration configuration)
    {
        builder.AddJwtBearer(SchemeName, options =>
        {
            var jwtSettings = configuration.GetSection(JwtSettings.SectionName).Get<JwtSettings>() ??
                              throw new NullReferenceException();

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true
            };

            options.RequireHttpsMetadata = false;
            options.SaveToken = true;

            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    var accessToken = context.Request.Query["access_token"];
                    var path = context.HttpContext.Request.Path;

                    if (!string.IsNullOrEmpty(accessToken) && (path.StartsWithSegments(ServerRoutes.HubPrefix)))
                    {
                        context.Token = accessToken;
                    }

                    return Task.CompletedTask;
                },

                OnTokenValidated = context =>
                {
                    var claims = context.Principal!.Claims.ToArray();
                    var newClaims = new ClaimsIdentity();

                    var userId = claims.First(x => x.Type.Equals(ClaimTypes.NameIdentifier, StringComparison.Ordinal)).Value;
                    var username = claims.First(x => x.Type.Equals(JwtClaimTypes.Name, StringComparison.Ordinal)).Value;
                    var nickname = claims.First(x => x.Type.Equals(JwtClaimTypes.NickName, StringComparison.Ordinal)).Value;

                    newClaims.AddClaim(new(UserClaimTypes.Id, userId));
                    newClaims.AddClaim(new(UserClaimTypes.Name, username));
                    newClaims.AddClaim(new(UserClaimTypes.NickName, nickname));

                    context.Principal!.AddIdentity(newClaims);

                    return Task.CompletedTask;
                }
            };
        });
    }
}