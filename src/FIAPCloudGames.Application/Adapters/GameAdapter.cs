using FIAPCloudGames.Application.Dtos;
using FIAPCloudGames.Domain.Entities;

namespace FIAPCloudGames.Application.Adapters
{
    /// <summary>
    /// Extension methods para conversão entre entidade Game e DTOs.
    /// </summary>
    public static class GameAdapter
    {
        /// <summary>
        /// Converte uma entidade Game para GameReadDto.
        /// </summary>
        public static GameReadDto ToDto(this Game game)
        {
            return new GameReadDto
            {
                Id = game.Id,
                Title = game.Title,
                Description = game.Description,
                ReleaseDate = game.ReleaseDate,
                Price = game.Price
            };
        }

        /// <summary>
        /// Converte uma coleção de entidades Game para GameReadDto.
        /// </summary>
        public static IEnumerable<GameReadDto> ToDto(this IEnumerable<Game> games)
        {
            return games.Select(g => g.ToDto());
        }
    }
}
