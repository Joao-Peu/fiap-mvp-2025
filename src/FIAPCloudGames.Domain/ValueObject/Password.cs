namespace FIAPCloudGames.Domain.ValueObject
{
    public class Password
    {
        private const int MininumLength = 8;

        public string Value { get; }
        public Password(string value)
        {
            if (!IsPasswordValid(value))
            {
                throw new ArgumentException("Senha deve ter no mínimo 8 caracteres, incluindo letras, números e caracteres especiais.");
            }

            Value = value;
        }

        public static bool IsPasswordValid(string password)
        {
            if (string.IsNullOrWhiteSpace(password) || password.Length < MininumLength)
            {
                return false;
            }

            var hasLetter = password.Any(char.IsLetter);
            var hasDigit = password.Any(char.IsDigit);
            var hasSpecial = password.Any(ch => !char.IsLetterOrDigit(ch));
            return hasLetter && hasDigit && hasSpecial;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
