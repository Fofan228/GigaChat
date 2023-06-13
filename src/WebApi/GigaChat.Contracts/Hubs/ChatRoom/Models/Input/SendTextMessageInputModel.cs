namespace GigaChat.Contracts.Hubs.ChatRoom.Models.Input;

public record SendTextMessageInputModel(Guid UserId, string Text, long ChatRoomId);