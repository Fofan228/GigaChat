using ErrorOr;

using GigaChat.Core.Common.Entities.ChatRooms;

using MediatR;

namespace GigaChat.Core.ChatRooms.Commands.OpenChatRoom;

public record OpenChatRoomCommand(Guid OwnerId, string Title, ICollection<Guid> UserIds)
    : IRequest<ErrorOr<OpenChatRoomCommandResult>>;