using GigaChat.Core.Common.Entities.ChatMessages;

using MediatR;

namespace GigaChat.Core.ChatMessages.Events;

public record EditTextMessageEvent(ChatMessage ChatMessage) : IRequest;