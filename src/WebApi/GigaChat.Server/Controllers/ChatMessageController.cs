using GigaChat.Contracts.Http.ChatMessages.Requests;
using GigaChat.Contracts.Http.ChatMessages.Responses;
using GigaChat.Contracts.Common.Routes;
using GigaChat.Core.ChatMessages.Commands.SendTextMessage;
using GigaChat.Core.ChatMessages.Queries.ListChatMessages;
using GigaChat.Server.Controllers.Common;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace GigaChat.Server.Controllers;

[Route(ServerRoutes.Controllers.ChatMessageController)]
public class ChatMessageController : ApiController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public ChatMessageController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<ListChatMessagesByChatRoomIdResponse>> ListChatMessagesByChatRoomId(
        [FromQuery] ListChatMessagesByChatRoomIdRequest request,
        CancellationToken cancellationToken)
    {
        var query = _mapper.Map<ListChatMessagesByChatRoomIdQuery>(request);
        var result = await _sender.Send(query, cancellationToken);
        if (result.IsError) return Problem(result.Errors);
        var response = _mapper.Map<ListChatMessagesByChatRoomIdResponse>(result.Value);
        return Ok(response);
    }
}