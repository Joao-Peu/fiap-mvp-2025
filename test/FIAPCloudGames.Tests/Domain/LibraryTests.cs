using Bogus;
using FIAPCloudGames.Domain.Entities;

namespace FIAPCloudGames.Tests.Domain;

public class LibraryTests
{
    [Fact]
    public async Task New_ShoulReturnLibrary()
    {
        var faker = new Faker();
        var user = new User(faker.Person.FullName, faker.Internet.Email(), "Valid@123");

        var library = new Library(user);

        Assert.NotNull(library);
        Assert.Equal(user, library.User);
        Assert.True(library.IsActive);
        Assert.Empty(library.OwnedGames);
    }

    [Fact]
    public async Task Delete_ShouldMarkLibraryAsDeleted()
    {
        var faker = new Faker();
        var user = new User(faker.Person.FullName, faker.Internet.Email(), "Valid@123");
        var library = new Library(user);

        library.Delete();

        Assert.False(library.IsActive);
    }

    [Fact]
    public async Task AddAcquiredGame_ShouldNotAddLibraryGame_WhenAlreadyExists()
    {
        var faker = new Faker();
        var user = new User(faker.Person.FullName, faker.Internet.Email(), "Valid@123");
        var library = new Library(user);
        var game = Game.New(Guid.NewGuid(), faker.Commerce.ProductName(), faker.Lorem.Sentence(), faker.Date.Recent(), faker.Random.Decimal(10, 100));
        var libraryGame = new LibraryGame(library, game);

        library.AddAcquiredGame(libraryGame);
        library.AddAcquiredGame(libraryGame);

        Assert.Single(library.OwnedGames);
    }

    [Fact]
    public async Task AddAcquiredGame_ShouldAddLibraryGame_WhenNotExists()
    {
        var faker = new Faker();
        var user = new User(faker.Person.FullName, faker.Internet.Email(), "Valid@123");
        var library = new Library(user);
        var game = Game.New(Guid.NewGuid(), faker.Commerce.ProductName(), faker.Lorem.Sentence(), faker.Date.Recent(), faker.Random.Decimal(10, 100));
        var libraryGame = new LibraryGame(library, game);

        library.AddAcquiredGame(libraryGame);

        Assert.Single(library.OwnedGames);
        Assert.Contains(libraryGame, library.OwnedGames);
    }

    [Fact]
    public async Task RemoveAcquiredGame_ShouldRemoveLibraryGame()
    {
        var faker = new Faker();
        var user = new User(faker.Person.FullName, faker.Internet.Email(), "Valid@123");
        var library = new Library(user);
        var game = Game.New(Guid.NewGuid(), faker.Commerce.ProductName(), faker.Lorem.Sentence(), faker.Date.Recent(), faker.Random.Decimal(10, 100));
        var libraryGame = new LibraryGame(library, game);

        library.AddAcquiredGame(libraryGame);
        library.RemoveAcquiredGame(libraryGame);

        Assert.Empty(library.OwnedGames);
    }
}
