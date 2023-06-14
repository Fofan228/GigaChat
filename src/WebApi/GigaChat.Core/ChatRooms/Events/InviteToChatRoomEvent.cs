using GigaChat.Core.Common.Entities.ChatRooms;
using GigaChat.Core.Common.Entities.Users;

using MediatR;

namespace GigaChat.Core.ChatRooms.Events;

public record InviteToChatRoomEvent(ChatRoom ChatRoom, User User) : IRequest;