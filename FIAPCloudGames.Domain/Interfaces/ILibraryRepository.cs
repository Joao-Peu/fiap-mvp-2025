using FIAPCloudGames.Domain.Entities;

namespace FIAPCloudGames.Domain.Interfaces;

public interface ILibraryRepository
{
    void Add(Library library);
    IEnumerable<Library> GetAll();
    Library? GetById(Guid id);
    Library? GetByUserId(Guid userId);
    void Remove(Library library);
    void Update(Library library);
}
