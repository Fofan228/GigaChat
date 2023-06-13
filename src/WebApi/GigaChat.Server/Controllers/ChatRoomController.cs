using GigaChat.Contracts.Common.Routes;
using GigaChat.Contracts.Http.ChatRooms.Requests;
using GigaChat.Contracts.Http.ChatRooms.Responses;
using GigaChat.Core.ChatRooms.Commands.CloseChatRoom;
using GigaChat.Core.ChatRooms.Commands.OpenChatRoom;
using GigaChat.Core.ChatRooms.Commands.UpdateChatRoomTitle;
using GigaChat.Core.ChatRooms.Queries.ListChatRooms;
using GigaChat.Server.Controllers.Common;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace GigaChat.Server.Controllers;

[Route(ServerRoutes.Controllers.ChatRoomController)]
public class ChatRoomController : ApiController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public ChatRoomController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpGet("{userId:guid}")]
    public async Task<ActionResult<IEnumerable<ChatRoomResponse>>> ListChatRooms(
        [FromRoute] Guid userId,
        CancellationToken cancellationToken)
    {
        var query = new ListUserChatRoomsQuery(userId);
        var result = await _sender.Send(query, cancellationToken);
        if (result.IsError) return Problem(result.Errors);
        var response = _mapper.Map<IEnumerable<ChatRoomResponse>>(result.Value);
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult> CreateChatRoom(
        OpenChatRoomCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(command, cancellationToken);
        return result.IsError ? Problem(result.Errors) : Created(string.Empty, null);
    }

    [HttpPatch]
    public async Task<ActionResult> UpdateChatRoomTitle(
        UpdateChatRoomTitleRequest request,
        CancellationToken cancellationToken)
    {
        var command = _mapper.Map<UpdateChatRoomTitleCommand>(request);
        var result = await _sender.Send(command, cancellationToken);
        return result.IsError ? Problem(result.Errors) : NoContent();
    }

    [HttpDelete]
    public async Task<ActionResult> SoftDeleteChatRoom(
        SoftDeleteChatRoomRequest request,
        CancellationToken cancellationToken)
    {
        var command = _mapper.Map<CloseChatRoomCommand>(request);
        var result = await _sender.Send(command, cancellationToken);
        return result.IsError ? Problem(result.Errors) : NoContent();
    }
}