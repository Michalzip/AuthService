using AuthService.Shared.ValueObjects.FirstName.Exceptions;

namespace AuthService.Shared.ValueObjects.FirstName
{
    public class FirstName : ValueObject
    {
        public string Value { get; }
        private const int MaxLength = 100;

        public FirstName(string value)
        {
            if (value.Length > MaxLength)
            {
                throw new InvalidFirstNameException(value);
            }

            Value = value;
        }
        public static implicit operator FirstName(string value) => value is null ? null : new FirstName(value);
        public static implicit operator string(FirstName value) => value?.Value;
    }
}