namespace FIAPCloudGames.Application.Dtos
{
    /// <summary>
    /// DTO para dados de usuário.
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// Nome do usuário.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// E-mail do usuário.
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Senha do usuário.
        /// </summary>
        public string Password { get; set; }
    }
}
