using ErrorOr;

namespace GigaChat.Core.Common.Errors;

public static partial class Errors
{
    public static class ChatRooms
    {
        public static Error RoomWithIdNotFound(long id) => Error.NotFound("Chat room", $"Chat room with id {id} not found");
        public static Error ChatRoomNotContainUserForSendMessage => Error.Conflict("Chat room", "Chat room not contain user for send message");
        public static Error UserIsNotOwnerForDelete => Error.Conflict("Delete chat room", "User is not owner for delete this chat room");
        public static Error UserIsNotOwnerForInvite => Error.Conflict("Invite chat room", "User is not owner for invite this chat room");
    }
}