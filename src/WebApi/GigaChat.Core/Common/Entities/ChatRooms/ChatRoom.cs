using GigaChat.Core.Common.Entities.Base;
using GigaChat.Core.Common.Entities.Users;

namespace GigaChat.Core.Common.Entities.ChatRooms;

public class ChatRoom : EntityBase<long>
{
    private ChatRoom()
    {
        Title = null!;
        Users = null!;
    }

    public ChatRoom(Guid ownerId, string title)
    {
        Title = title;
        Users = new List<User>();
        IsDeleted = false;
        OwnerId = ownerId;
    }

    public string Title { get; set; }
    public Guid OwnerId { get; set; }
    public List<User> Users { get; set; }
    public bool IsDeleted { get; set; }
}