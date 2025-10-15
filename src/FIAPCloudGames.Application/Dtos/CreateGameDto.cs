namespace FIAPCloudGames.Application.Dtos
{
    /// <summary>
    /// DTO para criar jogo.
    /// </summary>
    public class CreateGameDto
    {
        /// <summary>
        /// Título do jogo.
        /// </summary>
        public required string Title { get; set; }
        /// <summary>
        /// Descrição do jogo.
        /// </summary>
        public required string Description { get; set; }
        /// <summary>
        /// Data de lançamento.
        /// </summary>
        public required DateTime ReleaseDate { get; set; }
        /// <summary>
        /// Preço do jogo.
        /// </summary>
        public required decimal Price { get; set; }
    }
}
