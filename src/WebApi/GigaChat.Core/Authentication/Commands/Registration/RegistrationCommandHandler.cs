using ErrorOr;

using GigaChat.Core.Common.Repositories.Common.Interfaces;
using GigaChat.Core.Common.Repositories.Interfaces;
using GigaChat.Core.Common.Services.Interfaces;
using GigaChat.Core.Common.Specifications.Users;
using GigaChat.Core.Common.Entities.Users;
using GigaChat.Core.Common.Errors;

using MediatR;

namespace GigaChat.Core.Authentication.Commands.Registration;

public class RegistrationCommandHandler : IRequestHandler<RegistrationCommand, ErrorOr<RegistrationResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHashProvider _hashProvider;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtTokenProvider _jwtTokenProvider;

    public RegistrationCommandHandler(
        IUserRepository userRepository,
        IPasswordHashProvider hashProvider,
        IUnitOfWork unitOfWork,
        IJwtTokenProvider jwtTokenProvider)
    {
        _hashProvider = hashProvider;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _jwtTokenProvider = jwtTokenProvider;
    }

    public async Task<ErrorOr<RegistrationResult>> Handle(RegistrationCommand request, CancellationToken cancellationToken)
    {
        var spec = new UserByLoginSpecification(request.Login);
        if (await _userRepository.ExistsAsync(spec, cancellationToken))
            return Errors.Users.UserWithLoginAlreadyExists(request.Login);

        var hashedPassword = _hashProvider.GetHash(request.Password);
        var user = new User(request.Name, request.Login, hashedPassword);

        await _userRepository.InsertAsync(user, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var token = await _jwtTokenProvider.GenerateTokenAsync(user, cancellationToken);

        return new RegistrationResult(token);
    }
}