using GigaChat.Contracts.Common.Dto;
using GigaChat.Contracts.Hubs.ChatRoom;
using GigaChat.Contracts.Hubs.ChatRoom.Models.Output;
using GigaChat.Core.ChatRooms.Events;
using GigaChat.Server.SignalR.Hubs.Chat;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.SignalR;

namespace GigaChat.Server.SignalR.EventHandlers.Chat;

public class OpenChatRoomEventHandler : IRequestHandler<OpenChatRoomEvent>
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

    public async Task Handle(OpenChatRoomEvent request, CancellationToken cancellationToken)
    {
        var chatRoomOutputDto = _mapper.Map<ChatRoomOutputDto>(request.ChatRoom);

        var sendInviteToChatRoomOutputModel = new SendInviteToChatRoomOutputModel(chatRoomOutputDto);
        var sendOpenChatRoomOutputModel = new SendOpenChatRoomOutputModel(chatRoomOutputDto);

        var userIds = request.ChatRoom.Users.Select(x => x.Id).ToList();

        //TODO Уведомление для создателя чата при создании комнаты
        await _hubContext.Clients
            .User(request.ChatRoom.OwnerId.ToString())
            .SendOpenChatRoom(sendOpenChatRoomOutputModel);

        //TODO Уведомление для участников чата при создании комнаты
        await _hubContext.Clients
            .Users(userIds.Select(x => x.ToString()))
            .SendInviteToChatRoom(sendInviteToChatRoomOutputModel);

        foreach (var userId in userIds)
        {
            if (ChatHub.ConnectionIds.TryGetValue(userId, out var connectionId))
            {
                await _hubContext.Groups.AddToGroupAsync(
                    connectionId,
                    request.ChatRoom.Id.ToString(),
                    cancellationToken);
            }
        }
    }
}