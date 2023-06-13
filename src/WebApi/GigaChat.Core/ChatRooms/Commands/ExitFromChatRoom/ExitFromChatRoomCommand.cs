using ErrorOr;

using MediatR;

namespace GigaChat.Core.ChatRooms.Commands.ExitFromChatRoom;

public record ExitFromChatRoomCommand(Guid UserId, long ChatRoomId)
    : IRequest<ErrorOr<Updated>>;