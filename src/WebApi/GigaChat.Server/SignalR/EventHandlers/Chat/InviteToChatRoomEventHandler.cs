using GigaChat.Contracts.Hubs.ChatRoom;
using GigaChat.Contracts.Hubs.ChatRoom.Models.Output;
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
        var user = request.User;
        var inviteOutputModel = new InviteToChatRoomOutputModel(chatRoom.Id, chatRoom.Title);
        var invitedUserToChatRoom = new InvitedUserToChatRoomOutputModel(user.Name, chatRoom.Title);
        var userIds = chatRoom.Users.Select(x => x.Id).ToList();
        
        //TODO Уведомление участника о приглашении в чат
        await _hubContext.Clients
            .User(user.Id.ToString())
            .SendInviteToChatRoom(inviteOutputModel);
        //TODO Уведомление всех участников о приглашённом человеке в чат
        await _hubContext.Clients
            .Users(userIds.Where(u => u != user.Id).Select(x => x.ToString()))
            .SendInvitedUserInChatRoom(invitedUserToChatRoom);
        
        if (ChatHub.ConnectionIds.TryGetValue(user.Id, out var connectionId))
            await _hubContext.Groups.AddToGroupAsync(connectionId, chatRoom.Id.ToString(), cancellationToken);
    }
}