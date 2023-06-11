using ErrorOr;

using MediatR;

namespace GigaChat.Core.ChatRooms.Commands.CloseChatRoom;

public record CloseChatRoomCommand(Guid UserId, long ChatRoomId)
    : IRequest<ErrorOr<CloseChatRoomResult>>;