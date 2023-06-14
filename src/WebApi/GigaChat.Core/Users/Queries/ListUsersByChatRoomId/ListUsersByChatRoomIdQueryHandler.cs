using ErrorOr;

using GigaChat.Core.Common.Repositories.Interfaces;
using GigaChat.Core.Common.Errors;

using MediatR;

namespace GigaChat.Core.Users.Queries.ListUsersByChatRoomId;

public class ListUsersByChatRoomIdQueryHandler : IRequestHandler<ListUsersByChatRoomIdQuery, ErrorOr<ListUsersByChatRoomIdQueryResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IChatRoomRepository _chatRoomRepository;

    public ListUsersByChatRoomIdQueryHandler(IUserRepository userRepository, IChatRoomRepository chatRoomRepository)
    {
        _userRepository = userRepository;
        _chatRoomRepository = chatRoomRepository;
    }

    public async Task<ErrorOr<ListUsersByChatRoomIdQueryResult>> Handle(ListUsersByChatRoomIdQuery request, CancellationToken cancellationToken)
    {
        var chatRoom = await _chatRoomRepository.FindOneByIdAsync(request.ChatRoomId, cancellationToken);
        if (chatRoom is null) return Errors.ChatRooms.RoomWithIdNotFound(request.ChatRoomId);
        return new ListUsersByChatRoomIdQueryResult(chatRoom.Users);
    }
}