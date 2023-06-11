namespace GigaChat.Contracts.Hubs.Chat.Models.Output;

public record ExitFromChatRoomOutputModel(long ChatRoomId, Guid UserId);