namespace FIAPCloudGames.Application.Dtos
{
    /// <summary>
    /// DTO para leitura de dados de jogo.
    /// </summary>
    public class GameReadDto
    {
        /// <summary>
        /// ID do jogo.
        /// </summary>
        public Guid Id { get; set; }

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
