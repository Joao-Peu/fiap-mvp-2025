using FIAPCloudGames.Application.Dtos;
using FIAPCloudGames.Domain.Entities;

namespace FIAPCloudGames.Application.Adapters
{
    /// <summary>
    /// Extension methods para conversão entre entidade Library e DTOs.
    /// </summary>
    public static class LibraryAdapter
    {
        /// <summary>
        /// Converte uma entidade LibraryGame para LibraryGameReadDto.
        /// </summary>
        public static LibraryGameReadDto ToDto(this LibraryGame libraryGame)
        {
            return new LibraryGameReadDto
            {
                Id = libraryGame.Id,
                Game = libraryGame.Game?.ToDto() ?? throw new InvalidOperationException("Game não foi carregado na consulta do LibraryGame")
            };
        }

        /// <summary>
        /// Converte uma entidade Library para LibraryReadDto.
        /// </summary>
        public static LibraryReadDto ToDto(this Library library)
        {
            return new LibraryReadDto
            {
                Id = library.Id,
                IsActive = library.IsActive,
                User = library.User?.ToDto() ?? throw new InvalidOperationException("User não foi carregado na consulta da Library"),
                OwnedGames = [.. library.OwnedGames?.Select(lg => lg.ToDto()) ?? []]
            };
        }

        /// <summary>
        /// Converte uma coleção de entidades Library para LibraryReadDto.
        /// </summary>
        public static IEnumerable<LibraryReadDto> ToDto(this IEnumerable<Library> libraries)
        {
            return libraries.Select(l => l.ToDto());
        }
    }
}
