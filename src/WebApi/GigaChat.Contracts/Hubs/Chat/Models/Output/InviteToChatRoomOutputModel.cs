namespace GigaChat.Contracts.Hubs.Chat.Models.Output;

public record InviteToChatRoomOutputModel(long ChatRoomId, Guid UserId, Guid OwnerId);