namespace FIAPCloudGames.Domain.ValueObject
{
    public class Password
    {
        public string Value { get; }
        public Password(string value)
        {
            if (!IsValidPassword(value))
                throw new ArgumentException("Senha deve ter no mínimo 8 caracteres, incluindo letras, números e caracteres especiais.");
            Value = value;
        }
        private bool IsValidPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
                return false;
            var hasLetter = password.Any(char.IsLetter);
            var hasDigit = password.Any(char.IsDigit);
            var hasSpecial = password.Any(ch => !char.IsLetterOrDigit(ch));
            return hasLetter && hasDigit && hasSpecial;
        }
        public override string ToString() => Value;
    }
}
