
using AuthService.Shared.ValueObjects.LastName.Exceptions;

namespace AuthService.Shared.ValueObjects.LastName
{
    public class LastName : ValueObject
    {
        public string Value { get; }
        private const int MaxLength = 100;

        public LastName(string value)
        {
            if (value.Length > MaxLength)
            {
                throw new InvalidLastNameException(value);
            }

            Value = value;
        }
        public static implicit operator LastName(string value) => value is null ? null : new LastName(value);
        public static implicit operator string(LastName value) => value?.Value;
    }


}