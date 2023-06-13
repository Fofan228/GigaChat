namespace GigaChat.Contracts.Hubs.ChatRoom.Models.Output;

public record SendTextMessageOutputModel(string Text, long ChatRoomId, Guid UserId, string UserName);