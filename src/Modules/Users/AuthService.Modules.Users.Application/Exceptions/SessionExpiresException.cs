namespace AuthService.Modules.Application.Exceptions
{
    public class SessionExpiresException : ExceptionBase
    {
        public SessionExpiresException() : base("Session expired sign in again")
        {
        }

        public override int StatusCode => StatusCodes.Status410Gone;
    }
}