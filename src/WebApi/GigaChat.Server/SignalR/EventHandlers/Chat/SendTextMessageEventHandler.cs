using GigaChat.Contracts.Hubs.ChatRoom;
using GigaChat.Contracts.Hubs.ChatRoom.Models.Output;
using GigaChat.Core.ChatRooms.Events;
using GigaChat.Server.SignalR.Hubs.Chat;

using MediatR;

using Microsoft.AspNetCore.SignalR;

namespace GigaChat.Server.SignalR.EventHandlers.Chat;

public class SendTextMessageEventHandler : IRequestHandler<SendTextMessageEvent>
{
    private readonly IHubContext<ChatHub, IChatClientHub> _hubContext;

    public SendTextMessageEventHandler(IHubContext<ChatHub, IChatClientHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task Handle(SendTextMessageEvent request, CancellationToken cancellationToken)
    {
        var outputModel = new SendTextMessageOutputModel(
            Text: request.ChatMessage.Text,
            ChatRoomId: request.ChatMessage.ChatRoomId,
            UserId: request.ChatMessage.UserId,
            UserName: request.User.Name);

        await _hubContext.Clients
            .Group(request.ChatMessage.ChatRoomId.ToString())
            .SendTextMessage(outputModel);
    }
}