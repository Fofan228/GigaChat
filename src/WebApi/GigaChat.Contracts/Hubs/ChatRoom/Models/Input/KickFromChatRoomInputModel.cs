namespace GigaChat.Contracts.Hubs.ChatRoom.Models.Input;

public record KickFromChatRoomInputModel(Guid UserId, long ChatRoomId);