using ErrorOr;

using MediatR;

namespace GigaChat.Core.ChatRooms.Commands.KickFromChatRoom;

public record KickFromChatRoomCommand(Guid OwnerId, Guid UserId, long ChatRoomId)
    : IRequest<ErrorOr<KickFromChatRoomResult>>;