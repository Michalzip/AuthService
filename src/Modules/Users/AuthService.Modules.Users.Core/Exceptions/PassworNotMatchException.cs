
namespace AuthService.Modules.Users.Core.Exceptions
{
    public class PasswordNotMatchException : ExceptionBase
    {
        public PasswordNotMatchException() : base("password  not match")
        {
        }

        public override int StatusCode => StatusCodes.Status400BadRequest;
    }
}