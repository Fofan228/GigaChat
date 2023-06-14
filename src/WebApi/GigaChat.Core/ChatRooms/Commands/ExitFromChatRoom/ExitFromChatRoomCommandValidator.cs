using FluentValidation;

namespace GigaChat.Core.ChatRooms.Commands.ExitFromChatRoom;

public class ExitFromChatRoomCommandValidator : AbstractValidator<ExitFromChatRoomCommand>
{
    public ExitFromChatRoomCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.ChatRoomId).NotEmpty();
    }
}