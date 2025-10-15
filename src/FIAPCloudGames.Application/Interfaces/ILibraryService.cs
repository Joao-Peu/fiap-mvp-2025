using FIAPCloudGames.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAPCloudGames.Application.Interfaces;

public interface ILibraryService
{
    Library Register(Guid userId);
    bool AcquireGame(Guid userId, Guid gameId);
    IEnumerable<Library> GetAll();
    Library GetLibraryByUserId(Guid userId);
    Library GetById(Guid id);
    bool Delete(Guid id);
}
