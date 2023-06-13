using GigaChat.Core.Common.Entities.ChatMessages;

using MediatR;

namespace GigaChat.Core.ChatRooms.Events;

public record SendTextMessageEvent(ChatMessage ChatMessage, string UserName) : IRequest;