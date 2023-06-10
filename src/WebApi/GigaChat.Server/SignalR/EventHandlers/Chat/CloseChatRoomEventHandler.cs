using GigaChat.Contracts.Hubs.Chat;
using GigaChat.Contracts.Hubs.Chat.Models.Output;
using GigaChat.Core.ChatRooms.Events;
using GigaChat.Server.SignalR.Hubs.Chat;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.SignalR;

namespace GigaChat.Server.SignalR.EventHandlers.Chat;

public class CloseChatRoomEventHandler : IRequestHandler<CloseChatRoomEvent>
{
    private readonly IHubContext<ChatHub, IChatClientHub> _hubContext;

    public CloseChatRoomEventHandler(IHubContext<ChatHub, IChatClientHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task Handle(CloseChatRoomEvent request, CancellationToken cancellationToken)
    {
        var chatRoom = request.ChatRoom;
        var outputModel = new CloseChatRoomOutputModel(chatRoom.Id);
        var userIds = chatRoom.Users.Select(x => x.Id).ToList();

        foreach (var userId in userIds)
        {
            if (ChatHub.ConnectionIds.TryGetValue(userId, out var connectionId))
                await _hubContext.Groups.RemoveFromGroupAsync(connectionId, chatRoom.Id.ToString(), cancellationToken);
        }

        await _hubContext.Clients
            .Users(userIds.Select(x => x.ToString()))
            .SendCloseChatRoom(outputModel);
    }
}