using GigaChat.Contracts.Hubs.ChatRoom.Models.Input;
using GigaChat.Core.ChatRooms.Commands.CloseChatRoom;
using GigaChat.Core.ChatRooms.Commands.ExitFromChatRoom;
using GigaChat.Core.ChatRooms.Commands.InviteToChatRoom;
using GigaChat.Core.ChatRooms.Commands.OpenChatRoom;

namespace GigaChat.Server.SignalR.Hubs.Chat;

public partial class ChatHub
{
    public async Task OpenChatRoom(OpenChatRoomInputModel inputModel)
    {
        var request = _mapper.Map<OpenChatRoomCommand>((GetUserId(), inputModel));
        await _sender.Send(request);
    }

    public async Task CloseChatRoom(CloseChatRoomInputModel inputModel)
    {
        var request = _mapper.Map<CloseChatRoomCommand>((GetUserId(), inputModel));
        await _sender.Send(request);
    }

    public async Task InviteToChatRoom(InviteToChatRoomInputModel inputModel)
    {
        var request = _mapper.Map<InviteToChatRoomCommand>((GetUserId(), inputModel));
        await _sender.Send(request);
    }

    public Task KickFromChatRoom()
    {
        //TODO Фронт
        return Task.CompletedTask;
    }

    public async Task ExitFromChatRoom(ExitFromChatRoomInputModel inputModel)
    {
        var request = _mapper.Map<ExitFromChatRoomCommand>((GetUserId(), inputModel));
        await _sender.Send(request);
    }
}