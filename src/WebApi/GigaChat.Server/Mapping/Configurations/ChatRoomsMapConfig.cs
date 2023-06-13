using GigaChat.Contracts.Hubs.ChatRoom.Models.Input;
using GigaChat.Core.ChatRooms.Commands.ExitFromChatRoom;
using GigaChat.Core.ChatRooms.Commands.InviteToChatRoom;
using GigaChat.Core.ChatRooms.Commands.OpenChatRoom;

using Mapster;

namespace GigaChat.Server.Mapping.Configurations;

public class ChatRoomsMapConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(Guid OwnerId, OpenChatRoomInputModel Model), OpenChatRoomCommand>()
            .Map(d => d.OwnerId, s => s.OwnerId)
            .Map(d => d, s => s.Model);

        config.NewConfig<(Guid OwnerId, InviteToChatRoomInputModel Model), InviteToChatRoomCommand>()
            .Map(d => d.OwnerId, s => s.OwnerId)
            .Map(d => d, s => s.Model);

        config.NewConfig<(Guid UserId, ExitFromChatRoomInputModel Model), ExitFromChatRoomCommand>()
            .Map(d => d.UserId, s => s.UserId)
            .Map(d => d, s => s.Model);
    }
}