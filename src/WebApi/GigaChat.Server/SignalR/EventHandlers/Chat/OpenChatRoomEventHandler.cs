using GigaChat.Contracts.Hubs.ChatRoom;
using GigaChat.Contracts.Hubs.ChatRoom.Models.Output;
using GigaChat.Core.ChatRooms.Events;
using GigaChat.Server.SignalR.Hubs.Chat;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.SignalR;

namespace GigaChat.Server.SignalR.EventHandlers.Chat;

public class OpenChatRoomEventHandler : IRequestHandler<OpenToChatRoomEvent>
{
    private readonly IHubContext<ChatHub, IChatClientHub> _hubContext;
    private readonly IMapper _mapper;

    public OpenChatRoomEventHandler(
        IHubContext<ChatHub, IChatClientHub> hubContext,
        IMapper mapper)
    {
        _hubContext = hubContext;
        _mapper = mapper;
    }

    public async Task Handle(OpenToChatRoomEvent request, CancellationToken cancellationToken)
    {
        var chatRoom = request.ChatRoom;
        var chatRoomOutputModel = new ChatRoomOutputModel(chatRoom.Id, chatRoom.Title);
        var inviteToChatRoomOutputModel = new InviteToChatRoomOutputModel(chatRoom.Id, chatRoom.Title);
        //TODO Наверное Users нужно перенести в IAsyncEnumerable ListUsers
        var userIds = chatRoom.Users.Select(x => x.Id).ToList();

        //TODO Уведомление для создателя чата при создании команты
        await _hubContext.Clients
            .User(chatRoom.OwnerId.ToString())
            .SendOpenChatRoom(chatRoomOutputModel);
        //TODO Уведомление для участников чата при создании команты
        await _hubContext.Clients
            .Users(userIds.Where(u => u != chatRoom.OwnerId).Select(x => x.ToString()))
            .SendInviteToChatRoom(inviteToChatRoomOutputModel);
        
        foreach (var userId in userIds)
        {
            if (ChatHub.ConnectionIds.TryGetValue(userId, out var connectionId))
                await _hubContext.Groups.AddToGroupAsync(connectionId, chatRoom.Id.ToString(), cancellationToken);
        }
    }
}