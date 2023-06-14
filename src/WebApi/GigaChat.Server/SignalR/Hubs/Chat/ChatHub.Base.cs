using System.Collections.Concurrent;

using ErrorOr;

using GigaChat.Contracts.Common.Dto;
using GigaChat.Contracts.Common.Routes;
using GigaChat.Contracts.Hubs.ChatRoom;
using GigaChat.Contracts.Hubs.ChatRoom.Models.Output;
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
    private static readonly ConcurrentDictionary<Guid, string> _connectionIds =
        new ConcurrentDictionary<Guid, string>();
    public static IReadOnlyDictionary<Guid, string> ConnectionIds => _connectionIds;

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

        if (result.IsError) await Clients.Caller.SendError(result.Errors.Select(e => e.ToString()));

        _connectionIds.AddOrUpdate(userId, Context.ConnectionId, (_, _) => Context.ConnectionId);

        foreach (var chatRoom in result.Value.ChatRooms)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatRoom.Id.ToString(), Context.ConnectionAborted);
        }

        var outputModel = _mapper.Map<SendUserChatRoomsOutputModel>(result.Value);
        await Clients.Caller.SendUserChatRooms(outputModel);
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        _connectionIds.TryRemove(GetUserId(), out _);
        return base.OnDisconnectedAsync(exception);
    }

    /// <exception cref="ArgumentNullException" />
    /// <exception cref="FormatException" />
    private Guid GetUserId() => Guid.Parse(Context.UserIdentifier!);

    private Task SendToCallerErrors(IEnumerable<Error> errors)
    {
        return Clients.Caller.SendError(errors.Select(e => e.ToString()));
    }
}