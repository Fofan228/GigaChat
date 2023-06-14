using FluentValidation;

namespace GigaChat.Core.ChatMessages.Commands.SendTextMessage;

public class SendTextMessageCommandValidator : AbstractValidator<SendTextMessageCommand>
{
    public SendTextMessageCommandValidator()
    {
        RuleFor(x => x.Text).NotEmpty();
        RuleFor(x => x.ChatRoomId).NotEmpty();
        RuleFor(x => x.UserId).NotEmpty();
    }
}