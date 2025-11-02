using FIAPCloudGames.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace FIAPCloudGames.Domain.ValueObject
{
    public record Email
    {
        public string Value { get; private set; }
        private Email()
        {
        }
        public Email(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || !IsValidEmail(value))
            {
                throw new EmailInvalidException(value);
            }

            Value = value;
        }
        private bool IsValidEmail(string email)
        {
            var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return regex.IsMatch(email);
        }
        public override string ToString()
        {
            return Value;
        }
    }
}
