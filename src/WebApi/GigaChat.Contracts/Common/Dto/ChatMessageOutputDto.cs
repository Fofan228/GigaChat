namespace GigaChat.Contracts.Common.Dto;

public record ChatMessageOutputDto(
    Guid Id,
    string Text,
    long ChatRoomId,
    Guid UserId);