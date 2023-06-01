namespace AuthService.Shared.ValueObjects.FirstName.Exceptions
{
    public class InvalidFirstNameException : ExceptionBase
    {
        public string FirstName { get; }

        public InvalidFirstNameException(string firstName) : base($"FirstName: '{firstName}' is invalid.")
        {
            FirstName = firstName;
        }
        public override int StatusCode => StatusCodes.Status406NotAcceptable;
    }
}