using GigaChat.Contracts.Common.Dto;
using GigaChat.Contracts.Common.Models;
using GigaChat.Contracts.Hubs.ChatRoom;
using GigaChat.Contracts.Hubs.ChatRoom.Models.Output;
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
        var chatRoomOutputDto = _mapper.Map<ChatRoomOutputDto>(request.ChatRoom);
        var userOutputDto = _mapper.Map<UserOutputDto>(request.User);

        var sendInviteToChatRoomOutputModel = new SendInviteToChatRoomOutputModel(chatRoomOutputDto);
        var joinedUserToChatRoomOutputModel = new JoinedUserToChatRoomOutputModel(userOutputDto);

        var userIds = request.ChatRoom.Users
            .Select(x => x.Id.ToString())
            .ToList();

        //TODO Уведомление участника о приглашении в чат
        await _hubContext.Clients
            .User(request.User.Id.ToString())
            .SendInviteToChatRoom(sendInviteToChatRoomOutputModel);

        //TODO Уведомление всех участников о приглашённом человеке в чат
        await _hubContext.Clients
            .Users(userIds)
            .SendJoinedUserToChatRoom(joinedUserToChatRoomOutputModel);

        if (ChatHub.ConnectionIds.TryGetValue(request.User.Id, out var connectionId))
        {
            await _hubContext.Groups.AddToGroupAsync(
                connectionId,
                request.ChatRoom.Id.ToString(),
                cancellationToken);
        }
    }
}