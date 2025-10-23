using FIAPCloudGames.Application.Dtos;
using FIAPCloudGames.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAPCloudGames.Application.Interfaces;

public interface ILibraryService
{
    Task<LibraryReadDto> RegisterAsync(Guid userId);
    Task<bool> AcquireGameAsync(Guid userId, Guid gameId);
    Task<IEnumerable<LibraryReadDto>> GetAllAsync();
    Task<LibraryReadDto> GetLibraryByUserIdAsync(Guid userId);
    Task<LibraryReadDto?> GetByIdAsync(Guid id);
    Task<bool> DeleteAsync(Guid id);
}
