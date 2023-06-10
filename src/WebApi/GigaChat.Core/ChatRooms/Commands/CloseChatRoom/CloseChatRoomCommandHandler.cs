using ErrorOr;

using GigaChat.Core.Common.Repositories.Common.Interfaces;
using GigaChat.Core.Common.Repositories.Interfaces;

using MediatR;

namespace GigaChat.Core.ChatRooms.Commands.CloseChatRoom;

public class CloseChatRoomCommandHandler : IRequestHandler<CloseChatRoomCommand, ErrorOr<CloseChatRoomResult>>
{
    private readonly IChatRoomRepository _chatRoomRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CloseChatRoomCommandHandler(IChatRoomRepository chatRoomRepository, IUnitOfWork unitOfWork)
    {
        _chatRoomRepository = chatRoomRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<CloseChatRoomResult>> Handle(CloseChatRoomCommand request, CancellationToken cancellationToken)
    {
        var chatRoom = await _chatRoomRepository.FindOneByIdAsync(request.ChatRoomId);
        if (chatRoom is null) throw new NotImplementedException();
        if (chatRoom.OwnerId != request.userId) throw new NotImplementedException();

        chatRoom.IsDeleted = true;

        await _chatRoomRepository.UpdateAsync(chatRoom, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new CloseChatRoomResult(chatRoom.Id);
    }
}