using ErrorOr;

using GigaChat.Core.Common.Entities.ChatMessages;

using MediatR;

namespace GigaChat.Core.ChatMessages.Queries.ListChatMessages;

public record ListChatMessagesByChatRoomIdQuery(long ChatRoomId) :
    IRequest<ErrorOr<ListChatMessagesByChatRoomIdQueryResult>>;