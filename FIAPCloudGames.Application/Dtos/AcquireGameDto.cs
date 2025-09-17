namespace FIAPCloudGames.Application.Dtos
{
    /// <summary>
    /// DTO para aquisição de jogo pelo usuário.
    /// </summary>
    public class AcquireGameDto
    {
        /// <summary>
        /// Id do usuário.
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// Id do jogo.
        /// </summary>
        public Guid GameId { get; set; }
    }
}
