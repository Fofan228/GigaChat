namespace GigaChat.Core.ChatRooms.Commands.InviteToChatRoom;

public record InviteToChatRoomResult(long ChatRoomId, Guid UserId, Guid OwnerId);