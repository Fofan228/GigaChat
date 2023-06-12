namespace GigaChat.Contracts.Hubs.Chat.Models.Output;

public record KickFromChatRoomOutputModel(long ChatRoomId, Guid UserId, Guid OwnerId);