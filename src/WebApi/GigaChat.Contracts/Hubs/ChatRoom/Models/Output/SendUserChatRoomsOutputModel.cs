using GigaChat.Contracts.Common.Dto;

namespace GigaChat.Contracts.Hubs.ChatRoom.Models.Output;

public record SendUserChatRoomsOutputModel(IEnumerable<ChatRoomOutputDto> chatRooms);