using FIAPCloudGames.Application.Adapters;
using FIAPCloudGames.Application.Dtos;
using FIAPCloudGames.Application.Interfaces;
using FIAPCloudGames.Domain.Entities;
using FIAPCloudGames.Domain.Interfaces;

namespace FIAPCloudGames.Application.Services;

public class LibraryService(ILibraryRepository libraryRepository, IUserRepository userRepository, IGameRepository gameRepository) : ILibraryService
{
    public async Task<LibraryReadDto?> GetByIdAsync(Guid id)
    {
        var library = await libraryRepository.GetByIdAsync(id);
        return library?.ToDto();
    }

    public async Task<LibraryReadDto> GetLibraryByUserIdAsync(Guid userId)
    {
        var library = await libraryRepository.GetByUserIdAsync(userId) ?? throw new Exception($"Não foi criada biblioteca para o usuário {userId}.");
        return library.ToDto();
    }

    public async Task<IEnumerable<LibraryReadDto>> GetAllAsync()
    {
        var libraries = await libraryRepository.GetAllAsync();
        return libraries.ToDto();
    }

    public async Task<LibraryReadDto> RegisterAsync(Guid userId)
    {
        var user = await userRepository.GetByIdAsync(userId) ?? throw new Exception($"Usuário não encontrado para o id {userId}");
        var existingLibrary = await libraryRepository.GetByUserIdAsync(userId);
        if (existingLibrary != null)
        {
            throw new Exception($"Já existe uma biblioteca criada para o usuário {user.Name}.");
        }

        var library = new Library(user);
        await libraryRepository.AddAsync(library);
        return library.ToDto();
    }

    public async Task<bool> AcquireGameAsync(Guid userId, Guid gameId)
    {
        var library = await libraryRepository.GetByUserIdAsync(userId) ?? throw new Exception("Não foi possível obter a biblioteca para o usuário.");
        var game = await gameRepository.GetByIdAsync(gameId) ?? throw new Exception($"Não foi possível encontrar o jogo com id '{gameId}'.");
        if (await libraryRepository.ContainsGameAsync(library.Id, gameId))
        {
            throw new Exception("O jogo já existe na biblioteca.");
        }

        var ownedGame = new LibraryGame(library, game);
        library.AddAcquiredGame(ownedGame);
        await libraryRepository.UpdateAsync(library);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var library = await libraryRepository.GetByIdAsync(id) ?? throw new Exception($"Biblioteca não foi encontrada com o id {id}");
        if (!library.IsActive)
        {
            return true;
        }

        library.Delete();
        await libraryRepository.UpdateAsync(library);
        return !library.IsActive;
    }
}
