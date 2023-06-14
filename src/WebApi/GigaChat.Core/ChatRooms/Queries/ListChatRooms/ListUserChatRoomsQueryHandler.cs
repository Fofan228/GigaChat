using ErrorOr;

using GigaChat.Core.Common.Repositories.Interfaces;
using GigaChat.Core.Common.Specifications.ChatRooms;
using GigaChat.Core.Common.Entities.ChatRooms;

using MediatR;

namespace GigaChat.Core.ChatRooms.Queries.ListChatRooms;

public class ListUserChatRoomsQueryHandler : IRequestHandler<ListUserChatRoomsQuery, ErrorOr<ListUserChatRoomsQueryResult>>
{
    private readonly IChatRoomRepository _chatRoomRepository;

    public ListUserChatRoomsQueryHandler(IChatRoomRepository chatRoomRepository)
    {
        _chatRoomRepository = chatRoomRepository;
    }

    public async Task<ErrorOr<ListUserChatRoomsQueryResult>> Handle(ListUserChatRoomsQuery request, CancellationToken cancellationToken)
    {
        var spec = new NotDeletedChatRoomsSpec() & new UserChatRoomsSpec(request.UserId);
        var chatRooms = await _chatRoomRepository.FindMany(spec).ToListAsync(cancellationToken);
        return new ListUserChatRoomsQueryResult(chatRooms);
    }
}