namespace AuthService.Shared.Clock
{
    public interface IClock
    {
        DateTime CurrentDateTime();
        DateTimeOffset CurrentDateTimeOffset();
    }
}