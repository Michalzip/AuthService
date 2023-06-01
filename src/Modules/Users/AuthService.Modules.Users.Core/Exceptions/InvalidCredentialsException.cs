

namespace AuthService.Modules.Users.Core.Exceptions
{
    public class InvalidCredentialsException : ExceptionBase
    {

        public InvalidCredentialsException() : base("Invalid credentials")
        {
        }

        public override int StatusCode => StatusCodes.Status400BadRequest;
    }

}