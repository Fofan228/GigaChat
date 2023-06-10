using System.Reflection;

using FluentValidation;

using GigaChat.Core.Common.Behaviors;
using GigaChat.Core.Common.Services.Interfaces;
using GigaChat.Core.Common.Services.Models;
using GigaChat.Core.Common.Services.Realisations;

using MediatR;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GigaChat.Core;

public static class Module
{
    public static IServiceCollection AddCore(
        this IServiceCollection services,
        IConfiguration configuration,
        Assembly[] assembliesForMediatRServices)
    {
        var assembly = typeof(Module).Assembly;

        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssembly(assembly);
            options.RegisterServicesFromAssemblies(assembliesForMediatRServices);
            options.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        });

        services.AddValidatorsFromAssembly(assembly);

        services.AddScoped<IJwtTokenProvider, JwtTokenProvider>();
        services.AddScoped<IDateTimeProvider, DateTimeTimeProvider>();
        services.AddScoped<IPasswordHashProvider, PasswordHashProvider>();

        services.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));

        return services;
    }
}