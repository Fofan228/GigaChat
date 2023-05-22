using GigaChat.Contracts.Common.Routes;
using GigaChat.Contracts.Hubs.Chat;
using GigaChat.Contracts.Hubs.Chat.Models.Input;
using GigaChat.Core.ChatRooms.Queries.ListChatRooms;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

using SignalRSwaggerGen.Attributes;

namespace GigaChat.Server.SignalR.Hubs.Chat;

[Authorize]
[SignalRHub(ServerRoutes.Hubs.ChatHub)]
public partial class ChatHub : Hub<IChatClientHub>
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;
    private readonly ILogger<ChatHub> _logger;

    public ChatHub(ISender sender, IMapper mapper, ILogger<ChatHub> logger)
    {
        _sender = sender;
        _logger = logger;
        _mapper = mapper;
    }

    public override async Task OnConnectedAsync()
    {
        var userId = GetUserId();

        var query = new ListUserChatRoomsQuery(userId);
        var result = await _sender.Send(query, Context.ConnectionAborted);

        if (result.IsError) return;
        foreach (var chatRoom in result.Value)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatRoom.Id.ToString(), Context.ConnectionAborted);
        }

        var chatRoomModels = _mapper.Map<IEnumerable<ChatRoomOutputModel>>(result.Value);
        await Clients.Caller.SendUserChatRooms(chatRoomModels);
    }

    /// <exception cref="ArgumentNullException" />
    /// <exception cref="FormatException" />
    private Guid GetUserId() => Guid.Parse(Context.UserIdentifier!);
}