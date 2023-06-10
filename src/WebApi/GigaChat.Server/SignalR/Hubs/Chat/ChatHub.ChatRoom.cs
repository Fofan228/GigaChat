using GigaChat.Contracts.Hubs.Chat.Models.Input;
using GigaChat.Contracts.Hubs.Chat.Models.Output;
using GigaChat.Core.ChatRooms.Commands.CloseChatRoom;
using GigaChat.Core.ChatRooms.Commands.OpenChatRoom;

namespace GigaChat.Server.SignalR.Hubs.Chat;

public partial class ChatHub
{
    public async Task OpenChatRoom(OpenChatRoomInputModel inputModel)
    {
        var request = _mapper.Map<OpenChatRoomCommand>((GetUserId(), inputModel));

        var result = await _sender.Send(request);
        if (result.IsError) return;

        var chatRoom = result.Value;
        var chatRoomUserIds = chatRoom.Users.Select(x => x.Id.ToString()).ToList();

        var outputModel = _mapper.Map<ChatRoomOutputModel>(chatRoom);
        await Clients.Users(chatRoomUserIds).SendInviteToChatRoom(outputModel);
    }

    public async Task CloseChatRoom(CloseChatRoomInputModel inputModel)
    {
        var request = _mapper.Map<CloseChatRoomCommand>((GetUserId(), inputModel));

        var result = await _sender.Send(request);
        if (result.IsError) return;

        var outputModel = _mapper.Map<CloseChatRoomOutputModel>(result.Value);
        await Clients.Group(inputModel.ChatRoomId.ToString()).SendCloseChatRoom(outputModel);
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