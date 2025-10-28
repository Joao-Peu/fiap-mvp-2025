namespace FIAPCloudGames.Application.Dtos
{
    /// <summary>
    /// DTO para leitura de dados de biblioteca.
    /// </summary>
    public class LibraryReadDto
    {
        /// <summary>
        /// ID da biblioteca.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Status ativo da biblioteca.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Dados do usuário dono da biblioteca.
        /// </summary>
        public UserReadDto User { get; set; }

        /// <summary>
        /// Lista de jogos na biblioteca.
        /// </summary>
        public ICollection<LibraryGameReadDto> OwnedGames { get; set; } = [];
    }
}
