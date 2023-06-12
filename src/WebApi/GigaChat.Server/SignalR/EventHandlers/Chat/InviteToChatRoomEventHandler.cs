using GigaChat.Contracts.Hubs.Chat;
using GigaChat.Contracts.Hubs.Chat.Models.Output;
using GigaChat.Core.ChatRooms.Events;
using GigaChat.Server.SignalR.Hubs.Chat;

using MediatR;

using Microsoft.AspNetCore.SignalR;

namespace GigaChat.Server.SignalR.EventHandlers.Chat;

public class InviteToChatRoomEventHandler : IRequestHandler<InviteToChatRoomEvent>
{
    private readonly IHubContext<ChatHub, IChatClientHub> _hubContext;

    public InviteToChatRoomEventHandler(IHubContext<ChatHub, IChatClientHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task Handle(InviteToChatRoomEvent request, CancellationToken cancellationToken)
    {
        var chatRoom = request.ChatRoom;
        var userId = request.UserId;
        var outputModel = new InviteToChatRoomOutputModel(chatRoom.Id, request.UserId, chatRoom.OwnerId);

        if (ChatHub.ConnectionIds.TryGetValue(userId, out var connectionId))
            await _hubContext.Groups.AddToGroupAsync(connectionId, chatRoom.Id.ToString(), cancellationToken);

        await _hubContext.Clients
            .Users(userId.ToString())
            .SendInviteToChatRoom(outputModel);
    }
}