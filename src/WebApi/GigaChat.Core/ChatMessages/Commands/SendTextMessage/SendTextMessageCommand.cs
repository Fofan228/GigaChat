using ErrorOr;

using GigaChat.Core.Common.Entities.ChatMessages;

using MediatR;

namespace GigaChat.Core.ChatMessages.Commands.SendTextMessage;

public record SendTextMessageCommand(string Text, long ChatRoomId, Guid UserId)
    : IRequest<ErrorOr<ChatMessage>>;