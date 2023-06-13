using GigaChat.Contracts.Hubs.ChatRoom.Models.Input;
using GigaChat.Core.ChatRooms.Commands.InviteToChatRoom;
using GigaChat.Core.ChatRooms.Commands.OpenChatRoom;

using Mapster;

namespace GigaChat.Server.Mapping.Configurations;

public class ChatRoomsMapConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(Guid id, OpenChatRoomInputModel model), OpenChatRoomCommand>()
            .Map(d => d.OwnerId, s => s.id)
            .Map(d => d, s => s.model);

        config.NewConfig<(Guid ownerId, InviteToChatRoomInputModel model), InviteToChatRoomCommand>()
            .Map(d => d.OwnerId, s => s.ownerId)
            .Map(d => d, s => s.model);
    }
}