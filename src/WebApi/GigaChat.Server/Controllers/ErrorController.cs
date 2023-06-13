using GigaChat.Contracts.Common.Routes;
using GigaChat.Contracts.Hubs.ChatRoom.Models.Input;
using GigaChat.Core.ChatRooms.Commands.OpenChatRoom;
using GigaChat.Server.Controllers.Common;

using MapsterMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GigaChat.Server.Controllers;

[Route(ServerRoutes.Controllers.ErrorController)]
public class ErrorController : ApiController
{
    private readonly IMapper _mapper;

    public ErrorController(IMapper mapper)
    {
        _mapper = mapper;
    }

    [HttpGet]
    [AllowAnonymous]
    public ActionResult Error()
    {
        return Problem();
    }

    [HttpPost]
    [AllowAnonymous]
    public ActionResult Test(OpenChatRoomInputModel inputModel)
    {
        var request = _mapper.Map<OpenChatRoomCommand>((Guid.NewGuid(), inputModel));
        return Ok();
    }
}