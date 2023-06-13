using GigaChat.Contracts.Http.ChatMessages.Requests;
using GigaChat.Contracts.Http.ChatMessages.Responses;
using GigaChat.Contracts.Hubs.ChatRoom.Models.Input;
using GigaChat.Core.ChatMessages.Commands.SendTextMessage;
using GigaChat.Core.ChatMessages.Queries.ListChatMessages;
using GigaChat.Core.Common.Entities.ChatMessages;

using Mapster;

namespace GigaChat.Server.Mapping.Configurations;

public class ChatMessagesMapConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(Guid userId, SendTextMessageInputModel model), SendTextMessageCommand>()
            .Map(d => d.UserId, s => s.userId)
            .Map(d => d, s => s.model);
    }
}