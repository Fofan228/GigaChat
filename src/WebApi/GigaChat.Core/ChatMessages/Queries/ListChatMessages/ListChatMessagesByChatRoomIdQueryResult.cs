using GigaChat.Core.Common.Entities.ChatMessages;

namespace GigaChat.Core.ChatMessages.Queries.ListChatMessages;

public record ListChatMessagesByChatRoomIdQueryResult(
    IEnumerable<ChatMessage> Messages,
    IEnumerable<string> Logins);