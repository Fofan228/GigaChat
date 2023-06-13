namespace GigaChat.Contracts.Hubs.Chat.Models.Input;

public record KickFromChatRoomInputModel(Guid UserId, long ChatRoomId);