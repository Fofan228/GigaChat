using GigaChat.Core.Common.Entities.ChatRooms;

using MediatR;

namespace GigaChat.Core.ChatRooms.Events;

public record ExitFromChatRoomEvent(ChatRoom ChatRoom, Guid UserId) : IRequest;