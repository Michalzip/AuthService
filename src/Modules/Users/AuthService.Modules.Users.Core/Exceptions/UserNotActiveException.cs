
namespace AuthService.Modules.Users.Core.Exceptions
{
    public class UserNotActiveException : ExceptionBase
    {
        public Guid UserId { get; }

        public UserNotActiveException(Guid userId) : base($"User with ID: '{userId}' is not active.")
        {
            UserId = userId;
        }

        public override int StatusCode => StatusCodes.Status400BadRequest;
    }
}