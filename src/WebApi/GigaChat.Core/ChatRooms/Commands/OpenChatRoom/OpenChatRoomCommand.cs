using ErrorOr;

using GigaChat.Core.Common.Entities.ChatRooms;

using MediatR;

namespace GigaChat.Core.ChatRooms.Commands.OpenChatRoom;

public record OpenChatRoomCommand(Guid OwnerId, string Title, IEnumerable<Guid> UserIds)
    : IRequest<ErrorOr<OpenChatRoomCommandResult>>;