namespace GigaChat.Contracts.Common.Routes;

public static class ServerRoutes
{
    public const string ServerPrefix = "";
    public const string ControllerPrefix = ServerPrefix;
    public const string HubPrefix = $"{ServerPrefix}/hubs";
    public const string HealthCheck = $"{ServerPrefix}/health";

    public static class Controllers
    {
        public const string ErrorController = $"{ControllerPrefix}/error";
        public const string UserController = $"{ControllerPrefix}/users";
        public const string ChatMessageController = $"{ControllerPrefix}/messages";
        public const string ChatRoomController = $"{ControllerPrefix}/rooms";
        public const string AuthenticationController = $"{ControllerPrefix}/auth";
    }

    public static class Hubs
    {
        public const string ChatHub = $"{HubPrefix}/chat";
    }
}