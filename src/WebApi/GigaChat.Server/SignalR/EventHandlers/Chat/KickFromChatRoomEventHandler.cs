using GigaChat.Contracts.Hubs.Chat;
using GigaChat.Contracts.Hubs.Chat.Models.Output;
using GigaChat.Core.ChatRooms.Events;
using GigaChat.Server.SignalR.Hubs.Chat;

using MediatR;

using Microsoft.AspNetCore.SignalR;

namespace GigaChat.Server.SignalR.EventHandlers.Chat;

public class KickFromChatRoomEventHandler : IRequestHandler<KickFromChatRoomEvent>
{
    private readonly IHubContext<ChatHub, IChatClientHub> _hubContext;

    public KickFromChatRoomEventHandler(IHubContext<ChatHub, IChatClientHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task Handle(KickFromChatRoomEvent request, CancellationToken cancellationToken)
    {
        var chatRoom = request.ChatRoom;
        var userId = request.UserId;
        var outputModel = new KickFromChatRoomOutputModel(chatRoom.Id, userId, chatRoom.OwnerId);

        if (ChatHub.ConnectionIds.TryGetValue(userId, out var connectionId))
            await _hubContext.Groups.RemoveFromGroupAsync(connectionId, chatRoom.Id.ToString(), cancellationToken);

        await _hubContext.Clients
            .Users(userId.ToString())
            .SendKickFromChatRoom(outputModel);
    }
}