using GigaChat.Contracts.Hubs.ChatRoom.Models.Output;

namespace GigaChat.Contracts.Hubs.ChatRoom;

public interface IChatClientHub
{
    Task SendError(string error);
    Task SendUserChatRooms(IEnumerable<ChatRoomOutputModel> chatRoomModels);
    Task SendOpenChatRoom(ChatRoomOutputModel chatRoomModel);
    Task SendCloseChatRoom(CloseChatRoomOutputModel outputModel);
    Task SendJoinToChatRoom(JoinToChatRoomOutputModel outputModel);
    Task SendExitFromChatRoom(ExitFromChatRoomOutputModel outputModel);
    Task SendInviteToChatRoom(InviteToChatRoomOutputModel outputModel);
    Task SendKickFromChatRoom(KickFromChatRoomOutputModel outputModel);
    Task SendTextMessage(SendTextMessageOutputModel messageOutputModel);
}