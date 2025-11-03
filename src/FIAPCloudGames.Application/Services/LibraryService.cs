using FIAPCloudGames.Application.Adapters;
using FIAPCloudGames.Application.Dtos;
using FIAPCloudGames.Application.Exceptions;
using FIAPCloudGames.Application.Interfaces;
using FIAPCloudGames.Domain.Entities;
using FIAPCloudGames.Domain.Exceptions;
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
        var user = await userRepository.GetByIdAsync(userId) ?? throw new EntityNotFoundException(userId, nameof(User));
        var existingLibrary = await libraryRepository.GetByUserIdAsync(userId);
        if (existingLibrary != null)
        {
            throw new DuplicateLibraryException();
        }

        var library = new Library(user);
        await libraryRepository.AddAsync(library);
        return library.ToDto();
    }

    public async Task<bool> AcquireGameAsync(Guid userId, Guid gameId)
    {
        // Buscar a biblioteca do usuário
        var library = await libraryRepository.GetByUserIdAsync(userId) 
            ?? throw new Exception("Não foi possível obter a biblioteca para o usuário.");
        
        // Verificar se o jogo existe
        var game = await gameRepository.GetByIdAsync(gameId) 
            ?? throw new EntityNotFoundException(gameId, nameof(Game));
        
        // Verificar se o jogo já está na biblioteca
        if (await libraryRepository.ContainsGameAsync(library.Id, gameId))
        {
            throw new DuplicateGameInLibraryException();
        }

        // Adicionar o jogo à biblioteca diretamente
        await libraryRepository.AddGameToLibraryAsync(library.Id, gameId);
        
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var library = await libraryRepository.GetByIdAsync(id) ?? throw new EntityNotFoundException(id, nameof(Library));
        if (!library.IsActive)
        {
            return true;
        }

        library.Delete();
        await libraryRepository.UpdateAsync(library);
        return !library.IsActive;
    }
}
