using GigaChat.Core.Common.Entities.ChatMessages;
using GigaChat.Core.Common.Entities.Users;

using MediatR;

namespace GigaChat.Core.ChatMessages.Events;

public record SendTextMessageEvent(ChatMessage ChatMessage, User User) : IRequest;