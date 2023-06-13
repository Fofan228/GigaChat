using GigaChat.Contracts.Common.Dto;

namespace GigaChat.Contracts.Http.ChatMessages.Responses;

public record ListChatMessagesByChatRoomIdResponse(IEnumerable<ChatMessageOutputDto> Messages);