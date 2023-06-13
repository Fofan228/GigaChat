using ErrorOr;

using MediatR;

namespace GigaChat.Core.Users.Queries.ListUsersByChatRoomId;

public record ListUsersByChatRoomIdQuery(long ChatRoomId) : IRequest<ErrorOr<ListUsersByChatRoomIdQueryResult>>;