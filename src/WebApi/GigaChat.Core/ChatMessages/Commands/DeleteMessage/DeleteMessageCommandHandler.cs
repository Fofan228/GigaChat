using ErrorOr;

using GigaChat.Core.ChatMessages.Events;
using GigaChat.Core.Common.Repositories.Common.Interfaces;
using GigaChat.Core.Common.Repositories.Interfaces;
using GigaChat.Core.Common.Errors;

using MediatR;

namespace GigaChat.Core.ChatMessages.Commands.DeleteMessage;

public class DeleteMessageCommandHandler : IRequestHandler<DeleteMessageCommand, ErrorOr<Updated>>
{
    private readonly ISender _sender;
    private readonly IChatMessageRepository _messageRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteMessageCommandHandler(
        ISender sender,
        IChatMessageRepository messageRepository,
        IUnitOfWork unitOfWork)
    {
        _sender = sender;
        _messageRepository = messageRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Updated>> Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
    {
        var message = await _messageRepository.FindOneByIdAsync(request.MessageId, cancellationToken);
        if (message is null) throw new NotImplementedException();

        if (message.UserId != request.UserId) return Errors.ChatMessages.UserIsNotOwnerForDelete;

        await _messageRepository.DeleteByIdAsync(message.Id, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var @event = new DeleteMessageEvent(message);
        await _sender.Send(@event, cancellationToken);

        return Result.Updated;
    }
}