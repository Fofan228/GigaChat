using ErrorOr;

using MediatR;

namespace GigaChat.Core.ChatRooms.Commands.InviteToChatRoom;

public record InviteToChatRoomCommand(Guid OwnerId, Guid UserId, long ChatRoomId)
    : IRequest<ErrorOr<InviteToChatRoomResult>>;