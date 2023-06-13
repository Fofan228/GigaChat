namespace GigaChat.Contracts.Hubs.ChatRoom.Models.Output;

public record ExitFromChatRoomOutputModel(long ChatRoomId, Guid UserId);