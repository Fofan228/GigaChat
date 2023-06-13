using GigaChat.Contracts.Hubs.ChatRoom.Models.Input;
using GigaChat.Core.ChatMessages.Commands.CreateChatMessage;

namespace GigaChat.Server.SignalR.Hubs.Chat;

public partial class ChatHub
{
    public async Task SendTextMessage(SendTextMessageInputModel inputModel)
    {
        var request = _mapper.Map<SendTextMessageCommand>((GetUserId(), inputModel));
        var result = await _sender.Send(request);
        if (result.IsError) return;
    }

    public Task EditTextMessage()
    {
        return Task.CompletedTask;
    }

    public Task DeleteMessage()
    {
        return Task.CompletedTask;
    }
}