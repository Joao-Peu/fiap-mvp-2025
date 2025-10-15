using FIAPCloudGames.Application.Interfaces;
using FIAPCloudGames.Domain.Entities;
using FIAPCloudGames.Domain.Interfaces;
using FIAPCloudGames.Domain.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FIAPCloudGames.Application.Services
{
    public class LibraryService(ILibraryRepository libraryRepository, IUserRepository userRepository, IGameRepository gameRepository) : ILibraryService
    {

        public Library? GetById(Guid id)
        {
            return libraryRepository.GetById(id);
        }
        public Library GetLibraryByUserId(Guid userId)
        {
            var library = libraryRepository.GetByUserId(userId) ?? throw new Exception($"Não foi criada biblioteca para o usuário {userId}.");
            return library;
        }

        public IEnumerable<Library> GetAll() => libraryRepository.GetAll();

        public Library Register(Guid userId)
        {
            var user = userRepository.GetById(userId) ?? throw new Exception($"Usuário não encontrado para o id {userId}");
            var existingLibrary = libraryRepository.GetByUserId(userId);
            if (existingLibrary != null)
            {
                throw new Exception($"Já existe uma biblioteca criada para o usuário {user.Name}.");
            }

            return new Library(user);
        }

        public bool AcquireGame(Guid userId, Guid gameId)
        {
            var library = libraryRepository.GetByUserId(userId) ?? throw new Exception("Não foi possível obter a biblioteca para o usuário.");
            var game = gameRepository.GetById(gameId) ?? throw new Exception($"Não foi possível encontrar o jogo com id '{gameId}'.");
            if (libraryRepository.ContainsGameAsync(library.Id, gameId))
            {
                throw new Exception("O jogo já existe na biblioteca.");
            }

            var ownedGame = new LibraryGame(library, game);
            library.AddAcquiredGame(ownedGame);
            libraryRepository.Update(library);
            return true;
        }

        public bool Delete(Guid id)
        {
            var library = libraryRepository.GetById(id) ?? throw new Exception($"Biblioteca não foi encontrada com o id {id}");
            if (library.IsDeleted)
            {
                return false;
            }

            return true;
        }
    }
}
