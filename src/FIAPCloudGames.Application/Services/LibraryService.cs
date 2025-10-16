using FIAPCloudGames.Application.Interfaces;
using FIAPCloudGames.Domain.Entities;
using FIAPCloudGames.Domain.Interfaces;
using FIAPCloudGames.Domain.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FIAPCloudGames.Application.Services;

public class LibraryService(ILibraryRepository libraryRepository, IUserRepository userRepository, IGameRepository gameRepository) : ILibraryService
{
    public async Task<Library?> GetByIdAsync(Guid id)
    {
        return await libraryRepository.GetByIdAsync(id);
    }

    public async Task<Library> GetLibraryByUserIdAsync(Guid userId)
    {
        var library = await libraryRepository.GetByUserIdAsync(userId) ?? throw new Exception($"Não foi criada biblioteca para o usuário {userId}.");
        return library;
    }

    public async Task<IEnumerable<Library>> GetAllAsync()
    {
        return await libraryRepository.GetAllAsync();
    }

    public async Task<Library> RegisterAsync(Guid userId)
    {
        var user = await userRepository.GetByIdAsync(userId) ?? throw new Exception($"Usuário não encontrado para o id {userId}");
        var existingLibrary = await libraryRepository.GetByUserIdAsync(userId);
        if (existingLibrary != null)
        {
            throw new Exception($"Já existe uma biblioteca criada para o usuário {user.Name}.");
        }

        var library = new Library(user);
        await libraryRepository.AddAsync(library);
        return library;
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
        library.Delete();
        await libraryRepository.UpdateAsync(library);
        return !library.IsDeleted;
    }
}
