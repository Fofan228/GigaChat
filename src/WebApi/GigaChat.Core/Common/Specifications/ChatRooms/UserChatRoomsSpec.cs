using System.Linq.Expressions;

using GigaChat.Core.Common.Entities.ChatRooms;

using LinqSpecs;

namespace GigaChat.Core.Common.Specifications.ChatRooms;

public class UserChatRoomsSpec : Specification<ChatRoom>
{
    private readonly Guid _userId;

    public UserChatRoomsSpec(Guid userId)
    {
        _userId = userId;
    }

    public override Expression<Func<ChatRoom, bool>> ToExpression()
    {
        return chatRoom => chatRoom.Users.Any(user => user.Id == _userId);
    }
}