using GigaChat.Server.Authentication.Schemes;

using Microsoft.AspNetCore.Authentication;

namespace GigaChat.Server.Authentication;

public static class Module
{
    public static IServiceCollection AddGigaChatAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var builder = services.AddAuthentication(options =>
        {
            options.DefaultScheme = JwtScheme.SchemeName;
        });

        builder.AddJwtScheme(configuration);

        return services;
    }
}