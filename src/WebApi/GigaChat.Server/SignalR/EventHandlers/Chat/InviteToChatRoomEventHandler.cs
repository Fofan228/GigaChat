using GigaChat.Contracts.Hubs.Chat;
using GigaChat.Contracts.Hubs.Chat.Models.Input;
using GigaChat.Core.ChatRooms.Events;
using GigaChat.Server.SignalR.Hubs.Chat;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.SignalR;

namespace GigaChat.Server.SignalR.EventHandlers.Chat;

public class InviteToChatRoomEventHandler : IRequestHandler<InviteToChatRoomEvent>
{
    private readonly IHubContext<ChatHub, IChatClientHub> _hubContext;
    private readonly IMapper _mapper;

    public InviteToChatRoomEventHandler(
        IHubContext<ChatHub, IChatClientHub> hubContext,
        IMapper mapper)
    {
        _hubContext = hubContext;
        _mapper = mapper;
    }

    public async Task Handle(InviteToChatRoomEvent request, CancellationToken cancellationToken)
    {
        var outputModel = _mapper.Map<ChatRoomOutputModel>(request.ChatRoom);
        var chatRoom = request.ChatRoom;
        //TODO Наверное Users нужно перенести в IAsyncEnumerable ListUsers
        var userIds = chatRoom.Users.Select(x => x.Id).ToList();

        foreach (var userId in userIds)
        {
            if (ChatHub.ConnectionIds.TryGetValue(userId, out var connectionId))
                await _hubContext.Groups.AddToGroupAsync(connectionId, chatRoom.Id.ToString(), cancellationToken);
        }

        await _hubContext.Clients
            .Users(userIds.Select(x => x.ToString()))
            .SendInviteToChatRoom(outputModel);
    }
}