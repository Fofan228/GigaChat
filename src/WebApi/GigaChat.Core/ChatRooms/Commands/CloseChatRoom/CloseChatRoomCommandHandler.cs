using ErrorOr;

using GigaChat.Core.ChatRooms.Events;
using GigaChat.Core.Common.Repositories.Common.Interfaces;
using GigaChat.Core.Common.Repositories.Interfaces;

using MediatR;

namespace GigaChat.Core.ChatRooms.Commands.CloseChatRoom;

public class CloseChatRoomCommandHandler : IRequestHandler<CloseChatRoomCommand, ErrorOr<CloseChatRoomResult>>
{
    private readonly ISender _sender;
    private readonly IChatRoomRepository _chatRoomRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CloseChatRoomCommandHandler(
        IChatRoomRepository chatRoomRepository,
        IUnitOfWork unitOfWork,
        ISender sender)
    {
        _chatRoomRepository = chatRoomRepository;
        _unitOfWork = unitOfWork;
        _sender = sender;
    }

    public async Task<ErrorOr<CloseChatRoomResult>> Handle(CloseChatRoomCommand request, CancellationToken cancellationToken)
    {
        var chatRoom = await _chatRoomRepository.FindOneByIdAsync(request.ChatRoomId, cancellationToken);
        if (chatRoom is null) throw new NotImplementedException();
        if (chatRoom.OwnerId != request.UserId) throw new NotImplementedException();

        chatRoom.IsDeleted = true;

        await _chatRoomRepository.UpdateAsync(chatRoom, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var closeChatRoomEvent = new CloseChatRoomEvent(chatRoom);
        await _sender.Send(closeChatRoomEvent, cancellationToken);

        return new CloseChatRoomResult(chatRoom.Id);
    }
}