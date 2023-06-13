using GigaChat.Contracts.Common.Models;

namespace GigaChat.Contracts.Http.Users.Responses;

public record ListUsersByChatRoomIdResponse(IEnumerable<UserOutputDto> Users);