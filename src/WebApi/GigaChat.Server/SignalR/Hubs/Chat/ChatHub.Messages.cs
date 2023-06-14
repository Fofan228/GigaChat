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
        var result = await _sender.Send(request);
        if (result.IsError) await SendToCallerErrors(result.Errors);
    }

    public async Task EditTextMessage(EditTextMessageInputModel inputModel)
    {
        var request = _mapper.Map<EditTextMessageCommand>((GetUserId(), inputModel));
        var result = await _sender.Send(request);
        if (result.IsError) await SendToCallerErrors(result.Errors);
    }

    public async Task DeleteMessage(DeleteMessageInputModel inputModel)
    {
        var request = _mapper.Map<DeleteMessageCommand>((GetUserId(), inputModel));
        var result = await _sender.Send(request);
        if (result.IsError) await SendToCallerErrors(result.Errors);
    }
}