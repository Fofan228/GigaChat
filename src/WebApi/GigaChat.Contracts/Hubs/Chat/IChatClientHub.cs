using GigaChat.Contracts.Hubs.Chat.Models.Input;

namespace GigaChat.Contracts.Hubs.Chat;

public interface IChatClientHub
{
    Task SendError(string error);
    Task SendUserChatRooms(IEnumerable<ChatRoomOutputModel> chatRoomModels);
    Task SendInviteToChatRoom(ChatRoomOutputModel chatRoomModel);
}