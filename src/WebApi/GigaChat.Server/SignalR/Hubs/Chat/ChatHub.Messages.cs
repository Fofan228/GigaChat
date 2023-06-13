using GigaChat.Contracts.Hubs.ChatRoom.Models.Input;
using GigaChat.Core.ChatMessages.Commands.SendTextMessage;

namespace GigaChat.Server.SignalR.Hubs.Chat;

public partial class ChatHub
{
    public async Task SendTextMessage(SendTextMessageInputModel inputModel)
    {
        var request = _mapper.Map<SendTextMessageCommand>((GetUserId(), inputModel));
        await _sender.Send(request);
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