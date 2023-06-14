using ErrorOr;

using GigaChat.Core.Common.Repositories.Interfaces;
using GigaChat.Core.Common.Services.Interfaces;
using GigaChat.Core.Common.Specifications.Users;
using GigaChat.Core.Common.Errors;

using MediatR;

namespace GigaChat.Core.Authentication.Queries.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<LoginResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenProvider _jwtTokenProvider;
    private readonly IPasswordHashProvider _passwordHashProvider;

    public LoginQueryHandler(
        IUserRepository userRepository,
        IJwtTokenProvider jwtTokenProvider,
        IPasswordHashProvider passwordHashProvider)
    {
        _userRepository = userRepository;
        _jwtTokenProvider = jwtTokenProvider;
        _passwordHashProvider = passwordHashProvider;
    }

    public async Task<ErrorOr<LoginResult>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var userByLoginSpec = new UserByLoginSpecification(request.Login);
        var user = await _userRepository.FindOneAsync(userByLoginSpec, cancellationToken);
        if (user is null) return Errors.Users.UserWithLoginNotFound(request.Login);

        var hashedPassword = _passwordHashProvider.GetHash(request.Password, user.Password.Salt);
        if (!string.Equals(hashedPassword.Hash, user.Password.Hash, StringComparison.Ordinal))
            return Errors.Users.IncorrectPassword;

        var token = await _jwtTokenProvider.GenerateTokenAsync(user, cancellationToken);

        return new LoginResult(token);
    }
}