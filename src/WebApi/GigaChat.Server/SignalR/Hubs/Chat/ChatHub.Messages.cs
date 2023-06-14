using GigaChat.Contracts.Hubs.ChatRoom.Models.Input;
using GigaChat.Core.ChatMessages.Commands.DeleteMessage;
using GigaChat.Core.ChatMessages.Commands.EditTextMessage;
using GigaChat.Core.ChatMessages.Commands.SendTextMessage;

namespace GigaChat.Server.SignalR.Hubs.Chat;

public partial class ChatHub
{
    public async Task SendTextMessage(SendTextMessageInputModel inputModel)
    {
        var request = _mapper.Map<SendTextMessageCommand>((GetUserId(), inputModel));
        await _sender.Send(request);
    }

    public async Task EditTextMessage(EditTextMessageInputModel inputModel)
    {
        var request = _mapper.Map<EditTextMessageCommand>((GetUserId(), inputModel));
        await _sender.Send(request);
    }

    public async Task DeleteMessage(DeleteMessageInputModel inputModel)
    {
        var request = _mapper.Map<DeleteMessageCommand>((GetUserId(), inputModel));
        await _sender.Send(request);
    }
}