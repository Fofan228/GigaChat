using GigaChat.Contracts.Hubs.Chat.Models.Input;
using GigaChat.Core.ChatRooms.Commands.OpenChatRoom;

using Mapster;

namespace GigaChat.Server.Mapping.Configurations;

public class ChatRoomsMapConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(Guid id, OpenChatRoomInputModel), OpenChatRoomCommand>().Map(m => m.OwnerId, s => s.id);
    }
}