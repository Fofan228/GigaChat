using GigaChat.Contracts.Http.ChatMessages.Requests;
using GigaChat.Contracts.Http.ChatMessages.Responses;
using GigaChat.Core.ChatMessages.Commands.CreateChatMessage;
using GigaChat.Core.ChatMessages.Queries.ListChatMessages;
using GigaChat.Core.Common.Entities.ChatMessages;

using Mapster;

namespace GigaChat.Server.Mapping.Configurations;

public class ChatMessagesMapConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ChatMessage, ChatMessageResponse>();
        config.NewConfig<CreateChatMessageRequest, SendTextMessageCommand>();
        config.NewConfig<ListChatMessagesRequest, ListChatMessagesQuery>();
    }
}