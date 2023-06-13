using ErrorOr;

using GigaChat.Core.Common.Repositories.Interfaces;
using GigaChat.Core.Common.Specifications.ChatMessages;
using GigaChat.Core.Common.Entities.ChatMessages;

using MediatR;

namespace GigaChat.Core.ChatMessages.Queries.ListChatMessages;

public class ListChatMessagesQueryHandler : IRequestHandler<ListChatMessagesByChatRoomIdQuery, ErrorOr<ListChatMessagesByChatRoomIdQueryResult>>
{
    private readonly IChatMessageRepository _chatMessageRepository;
    private readonly IUserRepository _userRepository;

    public ListChatMessagesQueryHandler(
        IChatMessageRepository chatMessageRepository,
        IUserRepository userRepository)
    {
        _chatMessageRepository = chatMessageRepository;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<ListChatMessagesByChatRoomIdQueryResult>> Handle(
        ListChatMessagesByChatRoomIdQuery request,
        CancellationToken cancellationToken)
    {
        var spec = new ChatMessagesByChatRoomIdSpec(request.ChatRoomId);
        var chatMessages = await _chatMessageRepository.FindMany(spec).ToListAsync(cancellationToken);

        var userIds = chatMessages.Select(c => c.UserId).ToList();
        var users = await _userRepository.FindManyByIds(userIds)
            .ToListAsync(cancellationToken);
        
        var logins = users.Select(u => u.Login).ToList();

        return new ListChatMessagesByChatRoomIdQueryResult(chatMessages, logins);
    }
}