
namespace AuthService.Shared.ValueObjects.Password.Exceptions
{
    public class InvalidPasswordException : ExceptionBase
    {
        public string Password { get; }

        public InvalidPasswordException(string password) : base($"Password: '{password}' is invalid.")
        {
            Password = password;
        }

        public override int StatusCode => StatusCodes.Status400BadRequest;
    }
}