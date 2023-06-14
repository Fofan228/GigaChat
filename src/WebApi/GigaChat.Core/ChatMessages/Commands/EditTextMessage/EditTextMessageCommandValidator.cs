using FluentValidation;

namespace GigaChat.Core.ChatMessages.Commands.EditTextMessage;

public class EditTextMessageCommandValidator : AbstractValidator<EditTextMessageCommand>
{
    public EditTextMessageCommandValidator()
    {
        RuleFor(x => x.Text).NotEmpty();
        RuleFor(x => x.TextMessageId).NotEmpty();
        RuleFor(x => x.UserId).NotEmpty();
    }
}