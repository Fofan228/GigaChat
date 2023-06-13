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
    [HttpGet]
    [AllowAnonymous]
    public ActionResult Error()
    {
        return Problem();
    }
}