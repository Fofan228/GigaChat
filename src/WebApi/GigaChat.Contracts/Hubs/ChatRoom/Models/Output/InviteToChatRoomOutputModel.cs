namespace GigaChat.Contracts.Hubs.ChatRoom.Models.Output;

public record InviteToChatRoomOutputModel(long ChatRoomId, Guid UserId, Guid OwnerId);