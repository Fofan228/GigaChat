using GigaChat.Contracts.Http.ChatRooms.Requests;
using GigaChat.Contracts.Http.ChatRooms.Responses;
using GigaChat.Contracts.Hubs.ChatRoom.Models.Input;
using GigaChat.Core.ChatRooms.Commands.OpenChatRoom;
using GigaChat.Core.ChatRooms.Commands.CloseChatRoom;
using GigaChat.Core.ChatRooms.Commands.UpdateChatRoomTitle;
using GigaChat.Core.Common.Entities.ChatRooms;

using Mapster;

namespace GigaChat.Server.Mapping.Configurations;

public class ChatRoomsMapConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(Guid id, OpenChatRoomInputModel), OpenChatRoomCommand>().Map(m => m.OwnerId, s => s.id);
    }
}