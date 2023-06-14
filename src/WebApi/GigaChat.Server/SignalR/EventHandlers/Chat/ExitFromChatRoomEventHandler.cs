using GigaChat.Contracts.Hubs.ChatRoom;
using GigaChat.Contracts.Hubs.ChatRoom.Models.Output;
using GigaChat.Core.ChatRooms.Events;
using GigaChat.Server.SignalR.Hubs.Chat;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.SignalR;

namespace GigaChat.Server.SignalR.EventHandlers.Chat;

public class ExitFromChatRoomEventHandler : IRequestHandler<ExitFromChatRoomEvent>
{
    private readonly IHubContext<ChatHub, IChatClientHub> _hubContext;
    private readonly IMapper _mapper;

    public ExitFromChatRoomEventHandler(IHubContext<ChatHub, IChatClientHub> hubContext, IMapper mapper)
    {
        _hubContext = hubContext;
        _mapper = mapper;
    }

    public async Task Handle(ExitFromChatRoomEvent request, CancellationToken cancellationToken)
    {
        var exitFromChatRoomOutputModel = new ExitFromChatRoomOutputModel(request.ChatRoom.Id);
        var exitedUserFromChatRoomOutputModel = new ExitedUserFromChatRoomOutputModel(request.User.Id);

        if (ChatHub.ConnectionIds.TryGetValue(request.User.Id, out var connectionId))
        {
            await _hubContext.Groups.RemoveFromGroupAsync(
                connectionId,
                request.ChatRoom.Id.ToString(),
                cancellationToken);
        }

        //Уведомление участника о выходе из чата
        await _hubContext.Clients
            .User(request.User.Id.ToString())
            .SendExitFromChatRoom(exitFromChatRoomOutputModel);

        //Уведомление всех участников о выходе из чата
        await _hubContext.Clients
            .Users(request.ChatRoom.Users.Select(x => x.Id.ToString()))
            .SendExitedUserFromChatRoom(exitedUserFromChatRoomOutputModel);
    }
}