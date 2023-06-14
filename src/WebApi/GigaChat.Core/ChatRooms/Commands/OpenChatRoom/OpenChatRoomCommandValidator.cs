using FluentValidation;

namespace GigaChat.Core.ChatRooms.Commands.OpenChatRoom;

public class OpenChatRoomCommandValidator : AbstractValidator<OpenChatRoomCommand>
{
    public OpenChatRoomCommandValidator()
    {
        RuleFor(x => x.OwnerId).NotEmpty();
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.UserIds).NotNull();
    }
}