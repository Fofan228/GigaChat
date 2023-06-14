using FluentValidation;

namespace GigaChat.Core.ChatRooms.Commands.CloseChatRoom;

public class CloseChatRoomCommandValidator : AbstractValidator<CloseChatRoomCommand>
{
    public CloseChatRoomCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.ChatRoomId).NotEmpty();
    }
}