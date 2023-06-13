namespace GigaChat.Contracts.Hubs.ChatRoom.Models.Input;

public record OpenChatRoomInputModel(string Title, ICollection<Guid> UserIds);