using GigaChat.Contracts.Common.Routes;
using GigaChat.Server.SignalR.Hubs.Chat;
using GigaChat.Server.SignalR.Services;

using Microsoft.AspNetCore.SignalR;

namespace GigaChat.Server.SignalR;

public static class Module
{
    public static IServiceCollection AddGigaChatSignalR(this IServiceCollection services)
    {
        services.AddSignalR();
        services.AddSingleton<IUserIdProvider, UserIdProvider>();
        return services;
    }

    public static void MapHubs(this IEndpointRouteBuilder app)
    {
        app.MapHub<ChatHub>(ServerRoutes.Hubs.ChatHub);
    }
}