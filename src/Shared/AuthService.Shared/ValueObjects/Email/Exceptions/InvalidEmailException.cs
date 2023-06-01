namespace AuthService.Shared.ValueObjects.Email.Exceptions
{
    public class InvalidEmailException : ExceptionBase
    {
        public string Email { get; }

        public InvalidEmailException(string email) : base($"Email: '{email}' is invalid.")
        {
            Email = email;
        }
        public override int StatusCode => StatusCodes.Status406NotAcceptable;
    }
}