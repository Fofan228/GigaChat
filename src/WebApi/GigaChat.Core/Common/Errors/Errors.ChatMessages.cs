using ErrorOr;

namespace GigaChat.Core.Common.Errors;

public static partial class Errors
{
    public static class ChatMessages
    {
        public static Error UserIsNotOwnerForDelete => Error.Conflict("Delete message", "User is not owner for delete this message");
        public static Error UserIsNotOwnerForEditText => Error.Conflict("Edit message", "User is not owner for edit this message");   
        public static Error ChatMessageWithIdNotFound(long id) => Error.NotFound("Chat message", $"Chat message with id {id} not found");
    }
}