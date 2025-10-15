using FIAPCloudGames.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAPCloudGames.Application.Interfaces;

public interface ILibraryService
{
    Task<Library> RegisterAsync(Guid userId);
    Task<bool> AcquireGameAsync(Guid userId, Guid gameId);
    Task<IEnumerable<Library>> GetAllAsync();
    Task<Library> GetLibraryByUserIdAsync(Guid userId);
    Task<Library?> GetByIdAsync(Guid id);
    Task<bool> DeleteAsync(Guid id);
}
