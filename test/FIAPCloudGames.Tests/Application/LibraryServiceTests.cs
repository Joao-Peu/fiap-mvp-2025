using Bogus;
using FIAPCloudGames.Application.Exceptions;
using FIAPCloudGames.Application.Services;
using FIAPCloudGames.Domain.Entities;
using FIAPCloudGames.Domain.Exceptions;
using FIAPCloudGames.Domain.Interfaces;
using NSubstitute;

namespace FIAPCloudGames.Tests.Application;

public class LibraryServiceTests
{
    private readonly IGameRepository _gameRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILibraryRepository _libraryRepository;
    private readonly LibraryService _libraryService;

    public LibraryServiceTests()
    {
        _gameRepository = Substitute.For<IGameRepository>();
        _userRepository = Substitute.For<IUserRepository>();
        _libraryRepository = Substitute.For<ILibraryRepository>();
        _libraryService = new LibraryService(_libraryRepository, _userRepository, _gameRepository);
    }

    [Fact]
    public async Task GetById_ShouldReturnNull_WhenIdNotFound()
    {
        var nonExistentId = Guid.NewGuid();
        _libraryRepository.GetByIdAsync(nonExistentId).Returns((Library?)null);

        var result = await _libraryService.GetByIdAsync(nonExistentId);

        Assert.Null(result);
    }

    [Fact]
    public async Task GetById_ShouldReturnLibrary_WhenIdFound()
    {
        var faker = new Faker();
        var user = new User(faker.Person.FullName, faker.Internet.Email(), "Valid@123");
        var library = new Library(user);
        var libraryId = library.Id;

        _libraryRepository.GetByIdAsync(libraryId).Returns(library);

        var result = await _libraryService.GetByIdAsync(libraryId);

        Assert.NotNull(result);
        Assert.Equal(libraryId, result.Id);
        Assert.NotNull(result.User);
        Assert.Equal(user.Id, result.User.Id);
        Assert.Equal(user.Name, result.User.Name);
        Assert.Equal(user.Email.Value, result.User.Email);
    }

    [Fact]
    public async Task GetLibraryByUserId_ShouldThrowException_WhenUserHasNoLibrary()
    {
        var userId = Guid.NewGuid();
        _libraryRepository.GetByUserIdAsync(userId).Returns((Library?)null);

        await Assert.ThrowsAsync<Exception>(async () => await _libraryService.GetLibraryByUserIdAsync(userId));
    }

    [Fact]
    public async Task GetLibraryByUserId_ShouldReturnLibrary_WhenUserHasLibrary()
    {
        var faker = new Faker();
        var user = new User(faker.Person.FullName, faker.Internet.Email(), "Valid@123");
        var library = new Library(user);

        _libraryRepository.GetByUserIdAsync(user.Id).Returns(library);

        var result = await _libraryService.GetLibraryByUserIdAsync(user.Id);

        Assert.NotNull(result);
        Assert.Equal(library.Id, result.Id);
        Assert.NotNull(result.User);
        Assert.Equal(user.Id, result.User.Id);
        Assert.Equal(user.Name, result.User.Name);
        Assert.Equal(user.Email.Value, result.User.Email);
    }

    [Fact]
    public async Task GetAll_ShouldReturnAllLibraries()
    {
        var faker = new Faker();
        var user1 = new User(faker.Person.FullName, faker.Internet.Email(), "Valid@123");
        var user2 = new User(faker.Person.FullName, faker.Internet.Email(), "Valid@456");
        var library1 = new Library(user1);
        var library2 = new Library(user2);
        var libraries = new List<Library> { library1, library2 };

        _libraryRepository.GetAllAsync().Returns(libraries);

        var result = await _libraryService.GetAllAsync();

        Assert.Equal(2, result.Count());
        var resultList = result.ToList();
        Assert.Equal(library1.Id, resultList[0].Id);
        Assert.Equal(library2.Id, resultList[1].Id);
        Assert.Equal(user1.Id, resultList[0].User.Id);
        Assert.Equal(user2.Id, resultList[1].User.Id);
    }


    [Fact]
    public async Task Register_ShouldThrowException_WhenUserIdNotFound()
    {
        var nonExistentUserId = Guid.NewGuid();
        _userRepository.GetByIdAsync(nonExistentUserId).Returns((User?)null);

        await Assert.ThrowsAsync<EntityNotFoundException>(async () => await _libraryService.RegisterAsync(nonExistentUserId));
    }

    [Fact]
    public async Task Register_ShouldThrowException_WhenUserAlreadyHasLibrary()
    {
        var faker = new Faker();
        var user = new User(faker.Person.FullName, faker.Internet.Email(), "Valid@123");
        var existingLibrary = new Library(user);

        _userRepository.GetByIdAsync(user.Id).Returns(user);
        _libraryRepository.GetByUserIdAsync(user.Id).Returns(existingLibrary);

        await Assert.ThrowsAsync<DuplicateLibraryException>(async () => await _libraryService.RegisterAsync(user.Id));
    }

    [Fact]
    public async Task Register_ShouldCreateLibraryForUser()
    {
        var faker = new Faker();
        var user = new User(faker.Person.FullName, faker.Internet.Email(), "Valid@123");

        _userRepository.GetByIdAsync(user.Id).Returns(user);
        _libraryRepository.GetByUserIdAsync(user.Id).Returns((Library?)null);

        var result = await _libraryService.RegisterAsync(user.Id);

        Assert.NotNull(result);
        Assert.NotNull(result.User);
        Assert.Equal(user.Id, result.User.Id);
        Assert.Equal(user.Name, result.User.Name);
        Assert.Equal(user.Email.Value, result.User.Email);
        await _libraryRepository.Received(1).AddAsync(Arg.Any<Library>());
    }

    [Fact]
    public async Task AcquireGame_ShouldThrowException_WhenUserNotFound()
    {
        var userId = Guid.NewGuid();
        var gameId = Guid.NewGuid();

        _libraryRepository.GetByUserIdAsync(userId).Returns((Library?)null);

        await Assert.ThrowsAsync<Exception>(async () => await _libraryService.AcquireGameAsync(userId, gameId));
    }

    [Fact]
    public async Task AcquireGame_ShouldThrowException_WhenGameNotFound()
    {
        var faker = new Faker();
        var user = new User(faker.Person.FullName, faker.Internet.Email(), "Valid@123");
        var library = new Library(user);
        var nonExistentGameId = Guid.NewGuid();

        _libraryRepository.GetByUserIdAsync(user.Id).Returns(library);
        _gameRepository.GetByIdAsync(nonExistentGameId).Returns((Game?)null);

        await Assert.ThrowsAsync<EntityNotFoundException>(async () => await _libraryService.AcquireGameAsync(user.Id, nonExistentGameId));
    }

    [Fact]
    public async Task AcquireGame_ShouldThrowException_WhenGameAlreadyExistsInLibrary()
    {
        var faker = new Faker();
        var user = new User(faker.Person.FullName, faker.Internet.Email(), "Valid@123");
        var library = new Library(user);
        var game = Game.New(Guid.NewGuid(), faker.Commerce.ProductName(), faker.Lorem.Sentence(), faker.Date.Recent(), faker.Random.Decimal(10, 100));

        _libraryRepository.GetByUserIdAsync(user.Id).Returns(library);
        _gameRepository.GetByIdAsync(game.Id).Returns(game);
        _libraryRepository.ContainsGameAsync(library.Id, game.Id).Returns(true);

        await Assert.ThrowsAsync<DuplicateGameInLibraryException>(async () => await _libraryService.AcquireGameAsync(user.Id, game.Id));
    }

    [Fact]
    public async Task AcquireGame_ShouldAddGameToLibrary()
    {
        var faker = new Faker();
        var user = new User(faker.Person.FullName, faker.Internet.Email(), "Valid@123");
        var library = new Library(user);
        var game = Game.New(Guid.NewGuid(), faker.Commerce.ProductName(), faker.Lorem.Sentence(), faker.Date.Recent(), faker.Random.Decimal(10, 100));

        _libraryRepository.GetByUserIdAsync(user.Id).Returns(library);
        _gameRepository.GetByIdAsync(game.Id).Returns(game);
        _libraryRepository.ContainsGameAsync(library.Id, game.Id).Returns(false);

        var result = await _libraryService.AcquireGameAsync(user.Id, game.Id);

        Assert.True(result);
        await _libraryRepository.Received(1).AddGameToLibraryAsync(library.Id, game.Id);
    }

    [Fact]
    public async Task Delete_ShouldThrowException_WhenLibraryNotFound()
    {
        var nonExistentId = Guid.NewGuid();
        _libraryRepository.GetByIdAsync(nonExistentId).Returns((Library?)null);

        await Assert.ThrowsAsync<EntityNotFoundException>(async () => await _libraryService.DeleteAsync(nonExistentId));
    }

    [Fact]
    public async Task Delete_ShouldMarkLibraryAsDeleted()
    {
        var faker = new Faker();
        var user = new User(faker.Person.FullName, faker.Internet.Email(), "Valid@123");
        var library = new Library(user);

        _libraryRepository.GetByIdAsync(library.Id).Returns(library);

        var result = await _libraryService.DeleteAsync(library.Id);

        Assert.True(result);
        Assert.False(library.IsActive);
        await _libraryRepository.Received(1).UpdateAsync(library);
    }

}
