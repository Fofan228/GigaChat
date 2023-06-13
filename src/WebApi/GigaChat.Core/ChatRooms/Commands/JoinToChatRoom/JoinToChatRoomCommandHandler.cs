using ErrorOr;

using GigaChat.Core.ChatRooms.Events;
using GigaChat.Core.Common.Repositories.Common.Interfaces;
using GigaChat.Core.Common.Repositories.Interfaces;

using MediatR;

namespace GigaChat.Core.ChatRooms.Commands.JoinToChatRoom;

public class JoinToChatRoomCommandHandler : IRequestHandler<JoinToChatRoomCommand, ErrorOr<JoinToChatRoomResult>>
{
    private readonly ISender _sender;
    private readonly IUserRepository _userRepository;
    private readonly IChatRoomRepository _chatRoomRepository;
    private readonly IUnitOfWork _unitOfWork;

    public JoinToChatRoomCommandHandler(
        ISender sender,
        IChatRoomRepository chatRoomRepository,
        IUnitOfWork unitOfWork,
        IUserRepository userRepository)
    {
        _sender = sender;
        _chatRoomRepository = chatRoomRepository;
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<JoinToChatRoomResult>> Handle(JoinToChatRoomCommand request,
        CancellationToken cancellationToken)
    {
        var chatRoom = await _chatRoomRepository.FindOneByIdAsync(request.ChatRoomId, cancellationToken);
        if (chatRoom is null) throw new NotImplementedException();

        var user = await _userRepository.FindOneByIdAsync(request.UserId, cancellationToken);
        if (user is null) throw new NotImplementedException();

        chatRoom.Users.Add(user);

        await _chatRoomRepository.UpdateAsync(chatRoom, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var joinToChatRoomEvent = new JoinToChatRoomEvent(chatRoom, user.Id);
        await _sender.Send(joinToChatRoomEvent, cancellationToken);

        return new JoinToChatRoomResult(chatRoom.Id, user.Id);
    }
}