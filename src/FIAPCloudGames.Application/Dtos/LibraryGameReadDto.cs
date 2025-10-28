namespace FIAPCloudGames.Application.Dtos
{
    /// <summary>
    /// DTO para leitura de jogo na biblioteca.
    /// </summary>
    public class LibraryGameReadDto
    {
        /// <summary>
        /// ID do registro.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Dados do jogo.
        /// </summary>
        public GameReadDto Game { get; set; }
    }
}
