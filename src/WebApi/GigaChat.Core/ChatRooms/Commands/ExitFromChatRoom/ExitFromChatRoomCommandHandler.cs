using ErrorOr;

using GigaChat.Core.ChatRooms.Events;
using GigaChat.Core.Common.Repositories.Common.Interfaces;
using GigaChat.Core.Common.Repositories.Interfaces;

using MediatR;

namespace GigaChat.Core.ChatRooms.Commands.ExitFromChatRoom;

public class ExitFromChatRoomCommandHandler : IRequestHandler<ExitFromChatRoomCommand, ErrorOr<ExitFromChatRoomResult>>
{
    private readonly ISender _sender;
    private readonly IUserRepository _userRepository;
    private readonly IChatRoomRepository _chatRoomRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ExitFromChatRoomCommandHandler(
        ISender sender,
        IUserRepository userRepository,
        IChatRoomRepository chatRoomRepository,
        IUnitOfWork unitOfWork)
    {
        _sender = sender;
        _userRepository = userRepository;
        _chatRoomRepository = chatRoomRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<ExitFromChatRoomResult>> Handle(ExitFromChatRoomCommand request,
        CancellationToken cancellationToken)
    {
        var chatRoom = await _chatRoomRepository.FindOneByIdAsync(request.ChatRoomId, cancellationToken);
        if (chatRoom is null) throw new NotImplementedException();

        var user = await _userRepository.FindOneByIdAsync(request.UserId, cancellationToken);
        if (user is null) throw new NotImplementedException();

        chatRoom.Users.Remove(user);

        await _chatRoomRepository.UpdateAsync(chatRoom, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var exitFromChatRoomEvent = new ExitFromChatRoomEvent(chatRoom, user.Id);
        await _sender.Send(exitFromChatRoomEvent, cancellationToken);

        return new ExitFromChatRoomResult(chatRoom.Id, user.Id);
    }
}