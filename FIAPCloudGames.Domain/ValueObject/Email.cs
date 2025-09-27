using System.Text.RegularExpressions;

namespace FIAPCloudGames.Domain.ValueObject
{
    public class Email
    {
        public string Value { get; }
        public Email(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || !IsValidEmail(value))
            {
                throw new ArgumentException("E-mail inválido.");
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
