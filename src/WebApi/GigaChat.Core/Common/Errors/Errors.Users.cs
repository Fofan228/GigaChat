using ErrorOr;

namespace GigaChat.Core.Common.Errors;

public static partial class Errors
{
    public static class Users
    {
        public static Error UserWithIdNotFound(Guid id) => Error.NotFound("User", $"User with id {id} not found");
        public static Error UserWithLoginAlreadyExists(string login) => Error.Conflict("User", $"User with login {login} already exists");
        public static Error IncorrectPassword => Error.Conflict("User", "Incorrect password");
        public static Error UserWithLoginNotFound(string login) => Error.NotFound("User", $"User with login {login} not found");
    }
}