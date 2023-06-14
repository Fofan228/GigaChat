using GigaChat.Core.Common.Entities.ChatRooms;

using MediatR;

namespace GigaChat.Core.ChatRooms.Events;

public record CloseChatRoomEvent(ChatRoom ChatRoom) : IRequest;