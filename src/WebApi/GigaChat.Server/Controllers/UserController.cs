using GigaChat.Contracts.Common.Routes;
using GigaChat.Contracts.Http.Users.Responses;
using GigaChat.Core.Users.Queries.ListUsers;
using GigaChat.Core.Users.Queries.ListUsersByChatRoomId;
using GigaChat.Server.Controllers.Common;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace GigaChat.Server.Controllers;

[Route(ServerRoutes.Controllers.UserController)]
public class UserController : ApiController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public UserController(IMapper mapper, ISender sender)
    {
        _mapper = mapper;
        _sender = sender;
    }

    [HttpGet]
    public async Task<ActionResult<ListUsersResponse>> ListUsers(CancellationToken cancellationToken)
    {
        var query = new ListUsersQuery();
        var result = await _sender.Send(query, cancellationToken);
        if (result.IsError) return Problem(result.Errors);
        var response = _mapper.Map<ListUsersResponse>(result.Value);
        return Ok(response);
    }

    [HttpGet("{chatRoomId:long}")]
    public async Task<ActionResult<ListUsersByChatRoomIdResponse>> ListUsersByChatRoomId(
        [FromRoute] long chatRoomId,
        CancellationToken cancellationToken)
    {
        var query = new ListUsersByChatRoomIdQuery(chatRoomId);
        var result = await _sender.Send(query, cancellationToken);
        if (result.IsError) return Problem(result.Errors);
        var response = _mapper.Map<ListUsersByChatRoomIdResponse>(result.Value);
        return Ok(response);
    }
}