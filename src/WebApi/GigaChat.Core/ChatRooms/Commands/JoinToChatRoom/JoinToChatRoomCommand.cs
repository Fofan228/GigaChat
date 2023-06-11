using ErrorOr;

using MediatR;

namespace GigaChat.Core.ChatRooms.Commands.JoinToChatRoom;

public record JoinToChatRoomCommand(Guid UserId, long ChatRoomId)
    : IRequest<ErrorOr<JoinToChatRoomResult>>;