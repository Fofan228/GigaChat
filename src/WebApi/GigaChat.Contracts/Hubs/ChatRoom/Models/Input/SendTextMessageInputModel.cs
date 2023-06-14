namespace GigaChat.Contracts.Hubs.ChatRoom.Models.Input;

public record SendTextMessageInputModel(string Text, long ChatRoomId);