using GigaChat.Core.Common.Entities.ChatMessages;
using GigaChat.Core.Common.Entities.ChatRooms;
using GigaChat.Core.Common.Entities.Users;

using MediatR;

namespace GigaChat.Core.ChatRooms.Events;

public record SendTextMessageEvent(ChatMessage ChatMessage, User User) : IRequest;