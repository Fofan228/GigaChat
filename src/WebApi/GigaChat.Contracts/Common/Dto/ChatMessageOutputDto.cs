namespace GigaChat.Contracts.Common.Dto;

public record ChatMessageOutputDto(
    long Id,
    string Text,
    long ChatRoomId,
    Guid UserId);