using GigaChat.Core.Common.Entities.ChatRooms;

namespace GigaChat.Core.ChatRooms.Queries.ListChatRooms;

public record ListUserChatRoomsQueryResult(IEnumerable<ChatRoom> ChatRooms);