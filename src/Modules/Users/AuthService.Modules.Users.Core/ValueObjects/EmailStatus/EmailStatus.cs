using AuthService.Modules.Users.Core.ValueObjects.EmailStatus.Exceptions;
using AuthService.Shared.Modules.ValueObjects.Common;

namespace AuthService.Modules.Users.Core.ValueObjects.EmailStatus
{
    public class EmailStatus : ValueObject
    {
        public string Code { get; }
        public string Name { get; }

        private EmailStatus() { }

        public EmailStatus(string code, string name)
        {
            var codeSupported = IsCodeSupported(code);
            if (!codeSupported)
                throw new UnsupportedEmailStatusException(code);

            Code = code;
            Name = name;
        }

        public static EmailStatus Create(string code, string name)
            => new(code, name);

        private static bool IsCodeSupported(string code)
            => AvailableEmailStatusCodes.AllCodes.Contains(code, StringComparer.InvariantCultureIgnoreCase);
    }

}