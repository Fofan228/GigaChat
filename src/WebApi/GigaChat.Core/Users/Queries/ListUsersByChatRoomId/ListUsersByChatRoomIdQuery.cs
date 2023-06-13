using ErrorOr;

using GigaChat.Core.Common.Entities.Users;

using MediatR;

namespace GigaChat.Core.Users.Queries.ListUsersByChatRoomId;

public record ListUsersByChatRoomIdQuery(long ChatRoomId) : IRequest<ErrorOr<IEnumerable<User>>>;