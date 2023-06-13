namespace GigaChat.Contracts.Hubs.ChatRoom.Models.Output;

public record KickFromChatRoomOutputModel(long ChatRoomId, Guid UserId, Guid OwnerId);