using ErrorOr;

using GigaChat.Core.ChatMessages.Events;
using GigaChat.Core.Common.Repositories.Common.Interfaces;
using GigaChat.Core.Common.Repositories.Interfaces;

using MediatR;

namespace GigaChat.Core.ChatMessages.Commands.EditTextMessage;

public class EditTextMessageCommandHandler : IRequestHandler<EditTextMessageCommand, ErrorOr<Updated>>
{
    private readonly ISender _sender;
    private readonly IChatMessageRepository _messageRepository;
    private readonly IUnitOfWork _unitOfWork;

    public EditTextMessageCommandHandler(
        ISender sender,
        IChatMessageRepository messageRepository,
        IUnitOfWork unitOfWork)
    {
        _sender = sender;
        _messageRepository = messageRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Updated>> Handle(EditTextMessageCommand request, CancellationToken cancellationToken)
    {
        var message = await _messageRepository.FindOneByIdAsync(request.TextMessageId, cancellationToken);
        if (message is null) throw new NotImplementedException();

        if (message.UserId != request.UserId) throw new NotImplementedException();  

        message.Text = request.Text;
        await _messageRepository.UpdateAsync(message, cancellationToken);

        var @event = new EditTextMessageEvent(message);
        await _sender.Send(@event, cancellationToken);

        return Result.Updated;
    }
}