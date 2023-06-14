using ErrorOr;

using MediatR;

namespace GigaChat.Core.ChatRooms.Queries.ListChatRooms;

public record ListUserChatRoomsQuery(Guid UserId) : IRequest<ErrorOr<ListUserChatRoomsQueryResult>>;