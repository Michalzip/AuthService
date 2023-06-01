namespace AuthService.Shared.ValueObjects.CreatedAt
{
    public class CreatedAt : ValueObject
    {
        public DateTime Value { get; }

        public CreatedAt(DateTime value)
        {
            Value = value;

        }
        public static implicit operator CreatedAt(DateTime value) => new(value);
        public static implicit operator DateTime(CreatedAt value) => value.Value;
    }
}