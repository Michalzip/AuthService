
namespace AuthService.Modules.Core.Exceptions
{
    public class EmailInUseException : ExceptionBase
    {

        public EmailInUseException() : base("Email is already in use")
        {
        }

        public override int StatusCode => StatusCodes.Status409Conflict;
    }
}