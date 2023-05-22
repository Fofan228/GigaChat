namespace GigaChat.Contracts.Hubs.Chat.Models.Input;

public record OpenChatRoomInputModel(string Title, ICollection<Guid> UserIds);