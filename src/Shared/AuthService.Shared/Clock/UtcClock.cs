

namespace AuthService.Shared.Clock
{
    public class UtcClock : IClock
    {
        public DateTime CurrentDateTime() => DateTime.UtcNow;
        public DateTimeOffset CurrentDateTimeOffset() => DateTimeOffset.UtcNow;
    }
}