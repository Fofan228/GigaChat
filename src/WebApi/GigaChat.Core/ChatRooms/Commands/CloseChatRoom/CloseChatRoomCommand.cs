using ErrorOr;

using MediatR;

namespace GigaChat.Core.ChatRooms.Commands.CloseChatRoom;

public record CloseChatRoomCommand(Guid userId, long ChatRoomId)
    : IRequest<ErrorOr<long>>;