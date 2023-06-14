using ErrorOr;

using GigaChat.Core.Common.Repositories.Interfaces;
using GigaChat.Core.Common.Specifications.Users;
using GigaChat.Core.Common.Entities.Users;

using MediatR;

namespace GigaChat.Core.Users.Queries.ListUsers;

public class ListUsersQueryHandler : IRequestHandler<ListUsersQuery, ErrorOr<ListUsersQueryResult>>
{
    private readonly IUserRepository _userRepository;

    public ListUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<ListUsersQueryResult>> Handle(
        ListUsersQuery request,
        CancellationToken cancellationToken)
    {
        var spec = new NotDeletedUsersSpec();
        var users = await _userRepository.FindMany(spec).ToListAsync(cancellationToken);
        return new ListUsersQueryResult(users);
    }
}