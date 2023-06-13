namespace GigaChat.Core.ChatRooms.Commands.KickFromChatRoom;

public record KickFromChatRoomResult(long ChatRoomId, Guid UserId, Guid OwnerId);