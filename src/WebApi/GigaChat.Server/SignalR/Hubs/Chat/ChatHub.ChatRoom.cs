using GigaChat.Contracts.Hubs.Chat.Models.Input;
using GigaChat.Core.ChatRooms.Commands.CloseChatRoom;
using GigaChat.Core.ChatRooms.Commands.OpenChatRoom;

namespace GigaChat.Server.SignalR.Hubs.Chat;

public partial class ChatHub
{
    public async Task OpenChatRoom(OpenChatRoomInputModel model)
    {
        var request = _mapper.Map<OpenChatRoomCommand>((GetUserId(), model));

        var result = await _sender.Send(request);
        if (result.IsError) return;

        var chatRoom = result.Value;
        var chatRoomUserIds = chatRoom.Users.Select(x => x.Id.ToString()).ToList();

        var chatRoomModel = _mapper.Map<ChatRoomOutputModel>(chatRoom);
        await Clients.Users(chatRoomUserIds).SendInviteToChatRoom(chatRoomModel);
    }

    public async Task CloseChatRoom(CloseChatRoomInputModel model)
    {
        var request = _mapper.Map<CloseChatRoomCommand>((GetUserId(), model));

        var result = await _sender.Send(request);
        if (result.IsError) return;

        var chatRoomId = result.Value;
        
    }

    public Task InviteToChatRoom()
    {
        return Task.CompletedTask;
    }

    public Task KickFromChatRoom()
    {
        return Task.CompletedTask;
    }

    public Task JoinToChatRoom()
    {
        return Task.CompletedTask;
    }

    public Task ExitFromChatRoom()
    {
        return Task.CompletedTask;
    }
}