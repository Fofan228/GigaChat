using ErrorOr;

using GigaChat.Core.Common.Entities.ChatRooms;

using MediatR;

namespace GigaChat.Core.ChatRooms.Queries.ListChatRooms;

public record ListUserChatRoomsQuery(Guid UserId) : IRequest<ErrorOr<IEnumerable<ChatRoom>>>;