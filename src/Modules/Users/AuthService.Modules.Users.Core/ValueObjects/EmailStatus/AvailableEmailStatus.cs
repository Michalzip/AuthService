using AuthService.Modules.Users.Core.ValueObjects.EmailStatus.Exceptions;

namespace AuthService.Modules.Users.Core.ValueObjects.EmailStatus
{
    public class AvailableEmailStatus
    {
        public static readonly EmailStatus NoData = EmailStatus.Create(nameof(NoData), "noData");
        public static readonly EmailStatus Sent = EmailStatus.Create(nameof(Sent), "Sent");
        public static readonly EmailStatus Error = EmailStatus.Create(nameof(Error), "Error");

        private static readonly HashSet<EmailStatus> All = new()
    {
        NoData,
        Sent,
        Error
    };

        public static EmailStatus GetStatus(string code)
        {
            var category = All.FirstOrDefault(q => q.Code.Equals(code, StringComparison.InvariantCultureIgnoreCase));
            if (category is null)
                throw new UnsupportedEmailStatusException(code);
            return category;
        }
    }
}