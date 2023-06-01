
namespace AuthService.Modules.Users.Core.ValueObjects.EmailStatus.Exceptions
{
    internal class UnsupportedEmailStatusException : ExceptionBase
    {
        public string Code { get; }
        public UnsupportedEmailStatusException(string code) : base($"Unsupported email status exception '{code}'")
        {
            Code = code;
        }
        public override int StatusCode => StatusCodes.Status404NotFound;
    }

}