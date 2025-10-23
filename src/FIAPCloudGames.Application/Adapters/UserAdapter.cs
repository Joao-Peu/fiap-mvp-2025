using FIAPCloudGames.Application.Dtos;
using FIAPCloudGames.Domain.Entities;

namespace FIAPCloudGames.Application.Adapters
{
    /// <summary>
    /// Extension methods para conversão entre entidade User e DTOs.
    /// </summary>
    public static class UserAdapter
    {
        /// <summary>
        /// Converte uma entidade User para UserReadDto.
        /// </summary>
        public static UserReadDto ToDto(this User user)
        {
            return new UserReadDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email.Value
            };
        }

        /// <summary>
        /// Converte uma coleção de entidades User para UserReadDto.
        /// </summary>
        public static IEnumerable<UserReadDto> ToDto(this IEnumerable<User> users)
        {
            return users.Select(u => u.ToDto());
        }
    }
}
