using GigaChat.Contracts.Common.Dto;
using GigaChat.Contracts.Hubs.ChatRoom;
using GigaChat.Contracts.Hubs.ChatRoom.Models.Output;
using GigaChat.Core.ChatMessages.Events;
using GigaChat.Server.SignalR.Hubs.Chat;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.SignalR;

namespace GigaChat.Server.SignalR.EventHandlers.Chat;

public class DeleteMessageEventHandler : IRequestHandler<DeleteMessageEvent>
{
    private readonly IHubContext<ChatHub, IChatClientHub> _hubContext;
    private readonly IMapper _mapper;

    public DeleteMessageEventHandler(
        IHubContext<ChatHub, IChatClientHub> hubContext,
        IMapper mapper)
    {
        _hubContext = hubContext;
        _mapper = mapper;
    }

    public async Task Handle(DeleteMessageEvent request, CancellationToken cancellationToken)
    {
        var chatMessageOutputDto = _mapper.Map<ChatMessageOutputDto>(request.ChatMessage);
        var outputModel = new DeleteMessageOutputModel(chatMessageOutputDto);

        await _hubContext.Clients
            .Group(request.ChatMessage.ChatRoomId.ToString())
            .SendDeleteMessage(outputModel);
    }
}