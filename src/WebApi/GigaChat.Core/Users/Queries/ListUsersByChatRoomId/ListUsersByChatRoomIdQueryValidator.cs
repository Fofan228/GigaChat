using FluentValidation;

namespace GigaChat.Core.Users.Queries.ListUsersByChatRoomId;

public class ListUsersByChatRoomIdQueryValidator : AbstractValidator<ListUsersByChatRoomIdQuery>
{
    public ListUsersByChatRoomIdQueryValidator()
    {
        RuleFor(x => x.ChatRoomId).NotEmpty();
    }
}