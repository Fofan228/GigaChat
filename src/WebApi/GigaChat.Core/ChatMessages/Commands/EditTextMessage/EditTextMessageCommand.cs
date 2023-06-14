using ErrorOr;

using MediatR;

namespace GigaChat.Core.ChatMessages.Commands.EditTextMessage;

public record EditTextMessageCommand(Guid UserId, long TextMessageId, string Text) : IRequest<ErrorOr<Updated>>;