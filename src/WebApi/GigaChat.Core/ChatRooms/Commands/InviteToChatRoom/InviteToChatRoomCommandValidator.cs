using FluentValidation;

namespace GigaChat.Core.ChatRooms.Commands.InviteToChatRoom;

public class InviteToChatRoomCommandValidator : AbstractValidator<InviteToChatRoomCommand>
{
    public InviteToChatRoomCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.ChatRoomId).NotEmpty();
        RuleFor(x => x.OwnerId).NotEmpty();
    }
}