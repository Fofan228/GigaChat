namespace GigaChat.Contracts.Hubs.ChatRoom.Models.Output;

public record JoinToChatRoomOutputModel(long ChatRoomId, Guid UserId);