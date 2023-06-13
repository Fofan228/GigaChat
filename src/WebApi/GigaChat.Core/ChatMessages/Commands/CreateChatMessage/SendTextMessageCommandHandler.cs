using ErrorOr;

using GigaChat.Core.Common.Repositories.Common.Interfaces;
using GigaChat.Core.Common.Repositories.Interfaces;
using GigaChat.Core.Common.Entities.ChatMessages;

using MediatR;

namespace GigaChat.Core.ChatMessages.Commands.CreateChatMessage;

public class SendTextMessageCommandHandler : IRequestHandler<SendTextMessageCommand, ErrorOr<ChatMessage>>
{
    private readonly IChatMessageRepository _chatMessageRepository;
    private readonly IChatRoomRepository _chatRoomRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SendTextMessageCommandHandler(
        IChatMessageRepository chatMessageRepository,
        IChatRoomRepository chatRoomRepository,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork)
    {
        _chatMessageRepository = chatMessageRepository;
        _chatRoomRepository = chatRoomRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<ChatMessage>> Handle(
        SendTextMessageCommand request,
        CancellationToken cancellationToken)
    {
        if (!await _chatRoomRepository.ExistsWithIdAsync(request.ChatRoomId, cancellationToken))
            throw new NotImplementedException();
        if (!await _userRepository.ExistsWithIdAsync(request.UserId, cancellationToken))
            throw new NotImplementedException();

        var chatMessage = new ChatMessage(request.Text, request.ChatRoomId, request.UserId);

        await _chatMessageRepository.InsertAsync(chatMessage, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return chatMessage;
    }
}