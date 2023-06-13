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
        var user = request.User;
        var chatMessage = request.ChatMessage;
        var outputModel = new SendTextMessageOutputModel(chatMessage.Text, chatMessage.ChatRoomId,
            chatMessage.UserId, user.Name);

        await _hubContext.Clients
            .Group(chatMessage.ChatRoomId.ToString())
            .SendTextMessage(outputModel);
    }
}