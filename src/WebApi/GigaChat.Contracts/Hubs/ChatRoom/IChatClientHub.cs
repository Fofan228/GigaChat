using GigaChat.Contracts.Common.Dto;
using GigaChat.Contracts.Hubs.ChatRoom.Models.Output;

namespace GigaChat.Contracts.Hubs.ChatRoom;

public interface IChatClientHub
{
    Task SendError(string[] errors);
    Task SendUserChatRooms(SendUserChatRoomsOutputModel outputModel);
    Task SendOpenChatRoom(SendOpenChatRoomOutputModel outputModel);
    Task SendCloseChatRoom(CloseChatRoomOutputModel outputModel);
    Task SendExitFromChatRoom(ExitFromChatRoomOutputModel outputModel);
    Task SendExitedUserFromChatRoom(ExitedUserFromChatRoomOutputModel outputModel);
    Task SendInviteToChatRoom(SendInviteToChatRoomOutputModel outputModel);
    Task SendKickFromChatRoom(KickFromChatRoomOutputModel outputModel);
    Task SendTextMessage(SendTextMessageOutputModel outputModel);
    Task SendEditTextMessage(EditTextMessageOutputModel outputModel);
    Task SendDeleteMessage(DeleteMessageOutputModel outputModel);
    Task SendJoinedUserToChatRoom(JoinedUserToChatRoomOutputModel outputModel);
}