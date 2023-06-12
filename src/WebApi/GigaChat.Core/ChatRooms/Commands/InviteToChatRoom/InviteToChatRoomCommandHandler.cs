using ErrorOr;

using GigaChat.Core.ChatRooms.Events;
using GigaChat.Core.Common.Entities.ChatRooms;
using GigaChat.Core.Common.Repositories.Common.Interfaces;
using GigaChat.Core.Common.Repositories.Interfaces;

using MediatR;

namespace GigaChat.Core.ChatRooms.Commands.InviteToChatRoom;

public class InviteToChatRoomCommandHandler : IRequestHandler<InviteToChatRoomCommand, ErrorOr<InviteToChatRoomResult>>
{
    private readonly ISender _sender;
    private readonly IUserRepository _userRepository;
    private readonly IChatRoomRepository _chatRoomRepository;
    private readonly IUnitOfWork _unitOfWork;

    public InviteToChatRoomCommandHandler(
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

    public async Task<ErrorOr<InviteToChatRoomResult>> Handle(InviteToChatRoomCommand request,
        CancellationToken cancellationToken)
    {
        var chatRoom = await _chatRoomRepository.FindOneByIdAsync(request.ChatRoomId, cancellationToken);
        if (chatRoom is null) throw new NotImplementedException();

        var user = await _userRepository.FindOneByIdAsync(request.UserId, cancellationToken);
        if (user is null) throw new NotImplementedException();

        var owner = await _userRepository.FindOneByIdAsync(request.OwnerId, cancellationToken);
        if (owner is null) throw new NotImplementedException();

        if (request.OwnerId == chatRoom.OwnerId)
            chatRoom.Users.Add(user);
        else
            throw new NotImplementedException();

        await _chatRoomRepository.UpdateAsync(chatRoom, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var inviteToChatRoomEvent = new InviteToChatRoomEvent(chatRoom, user.Id);
        await _sender.Send(inviteToChatRoomEvent, cancellationToken);

        return new InviteToChatRoomResult(chatRoom.Id, user.Id, chatRoom.OwnerId);
    }
}