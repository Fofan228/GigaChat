using ErrorOr;

using MediatR;

namespace GigaChat.Core.ChatMessages.Commands.DeleteMessage;

public record DeleteMessageCommand(Guid UserId, long MessageId) : IRequest<ErrorOr<Updated>>;