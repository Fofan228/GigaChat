using ErrorOr;

using GigaChat.Core.ChatRooms.Events;
using GigaChat.Core.Common.Repositories.Common.Interfaces;
using GigaChat.Core.Common.Repositories.Interfaces;
using GigaChat.Core.Common.Errors;

using MediatR;

namespace GigaChat.Core.ChatRooms.Commands.ExitFromChatRoom;

public class ExitFromChatRoomCommandHandler : IRequestHandler<ExitFromChatRoomCommand, ErrorOr<Updated>>
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

    public async Task<ErrorOr<Updated>> Handle(ExitFromChatRoomCommand request,
        CancellationToken cancellationToken)
    {
        var chatRoom = await _chatRoomRepository.FindOneByIdAsync(request.ChatRoomId, cancellationToken);
        if (chatRoom is null) return Errors.ChatRooms.RoomWithIdNotFound(request.ChatRoomId);

        var user = chatRoom.Users.FirstOrDefault(u => u.Id == request.UserId);
        if (user is null) return Errors.Users.UserWithIdNotFound(request.UserId);

        chatRoom.Users.Remove(user);

        await _chatRoomRepository.UpdateAsync(chatRoom, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var exitFromChatRoomEvent = new ExitFromChatRoomEvent(chatRoom, user);
        await _sender.Send(exitFromChatRoomEvent, cancellationToken);

        return Result.Updated;
    }
}