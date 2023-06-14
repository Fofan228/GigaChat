using FluentValidation;

namespace GigaChat.Core.ChatMessages.Queries.ListChatMessages;

public class ListChatMessagesByChatRoomIdQueryValidator : AbstractValidator<ListChatMessagesByChatRoomIdQuery>
{
    public ListChatMessagesByChatRoomIdQueryValidator()
    {
        RuleFor(x => x.ChatRoomId).NotEmpty();
    }
}