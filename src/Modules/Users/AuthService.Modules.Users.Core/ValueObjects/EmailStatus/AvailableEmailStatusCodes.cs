
namespace AuthService.Modules.Users.Core.ValueObjects.EmailStatus
{
    public class AvailableEmailStatusCodes
    {
        public static readonly string NoData = nameof(NoData);
        public static readonly string Sent = nameof(Sent);
        public static readonly string Error = nameof(Error);

        public static readonly IReadOnlyCollection<string> AllCodes = new List<string>
    {
        NoData,
        Sent,
        Error,
    };


    }
}