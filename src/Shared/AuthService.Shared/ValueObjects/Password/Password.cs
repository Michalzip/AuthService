

using AuthService.Shared.ValueObjects.Password.Exceptions;

namespace AuthService.Shared.ValueObjects.Password
{
    public class Password : ValueObject
    {
        public string Value { get; }
        private const int MaxLength = 500;

        public Password(string value)
        {
            if (value.Length > MaxLength)
            {
                throw new InvalidPasswordException(value);
            }

            Value = value;
        }
        public static implicit operator Password(string value) => value is null ? null : new Password(value);
        public static implicit operator string(Password value) => value?.Value;
    }
}