using GigaChat.Core.Common.Entities.Users;

namespace GigaChat.Core.Users.Queries.ListUsers;

public record ListUsersQueryResult(IEnumerable<User> Users);