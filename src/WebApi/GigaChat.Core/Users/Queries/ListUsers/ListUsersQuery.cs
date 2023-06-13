using ErrorOr;

using GigaChat.Core.Common.Entities.Users;

using MediatR;

namespace GigaChat.Core.Users.Queries.ListUsers;

public record ListUsersQuery : IRequest<ErrorOr<ListUsersQueryResult>>;