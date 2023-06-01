
namespace AuthService.Shared.ValueObjects.LastName.Exceptions
{
    public class InvalidLastNameException : ExceptionBase
    {
        public string LastName { get; }
        public InvalidLastNameException(string lastName) : base($"LastName: '{lastName}' is invalid.")
        {
            LastName = lastName;
        }

        public override int StatusCode => StatusCodes.Status406NotAcceptable;

    }
}