using GigaChat.Core.Common.Entities.Users;

namespace GigaChat.Core.Users.Queries.ListUsersByChatRoomId;

public record ListUsersByChatRoomIdQueryResult(IEnumerable<User> Users);