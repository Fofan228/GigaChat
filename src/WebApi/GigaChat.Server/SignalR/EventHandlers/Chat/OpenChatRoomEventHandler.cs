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
            .SendOpenChatRoom(outputModel);
    }
}