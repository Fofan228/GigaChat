using GigaChat.Contracts.Http.ChatRooms.Requests;
using GigaChat.Contracts.Http.ChatRooms.Responses;
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
        config.NewConfig<ChatRoom, ChatRoomResponse>();
        config.NewConfig<CreateChatRoomRequest, OpenChatRoomCommand>();
        config.NewConfig<UpdateChatRoomTitleRequest, UpdateChatRoomTitleCommand>();
        config.NewConfig<SoftDeleteChatRoomRequest, CloseChatRoomCommand>();
    }
}