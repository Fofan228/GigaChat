using GigaChat.Contracts.Hubs.Chat.Models.Input;
using GigaChat.Contracts.Hubs.Chat.Models.Output;

namespace GigaChat.Contracts.Hubs.Chat;

public interface IChatClientHub
{
    Task SendError(string error);
    Task SendUserChatRooms(IEnumerable<ChatRoomOutputModel> chatRoomModels);
    Task SendInviteToChatRoom(ChatRoomOutputModel chatRoomModel);
    Task SendCloseChatRoom(CloseChatRoomOutputModel outputModel);
    Task SendJoinToChatRoom(JoinToChatRoomOutputModel outputModel);
    Task SendExitFromChatRoom(ExitFromChatRoomOutputModel outputModel);
}