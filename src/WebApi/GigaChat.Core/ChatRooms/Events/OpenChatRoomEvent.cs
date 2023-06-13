using GigaChat.Core.Common.Entities.ChatRooms;

using MediatR;

namespace GigaChat.Core.ChatRooms.Events;

public record OpenChatRoomEvent(ChatRoom ChatRoom) : IRequest;