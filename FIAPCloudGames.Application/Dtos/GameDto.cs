namespace FIAPCloudGames.Application.Dtos
{
    /// <summary>
    /// DTO para dados de jogo.
    /// </summary>
    public class GameDto
    {
        /// <summary>
        /// Título do jogo.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Descrição do jogo.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Data de lançamento.
        /// </summary>
        public DateTime ReleaseDate { get; set; }
        /// <summary>
        /// Preço do jogo.
        /// </summary>
        public decimal Price { get; set; }
    }
}
