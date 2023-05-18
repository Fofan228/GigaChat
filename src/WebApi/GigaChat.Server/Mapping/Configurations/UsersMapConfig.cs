using GigaChat.Contracts.Authentication.Requests;
using GigaChat.Contracts.Users.Requests;
using GigaChat.Contracts.Users.Responses;
using GigaChat.Core.Authentication.Commands.Registration;
using GigaChat.Core.Entities.Users;
using GigaChat.Core.Users.Commands.SoftDeleteUser;
using GigaChat.Core.Users.Commands.UpdateUsername;

using Mapster;

namespace GigaChat.Server.Mapping.Configurations;

public class UsersMapConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<UpdateUsernameCommand, UpdateUsernameRequest>();
        config.NewConfig<SoftDeleteUserCommand, SoftDeleteUserRequest>();
        config.NewConfig<User, UserResponse>();
    }
}