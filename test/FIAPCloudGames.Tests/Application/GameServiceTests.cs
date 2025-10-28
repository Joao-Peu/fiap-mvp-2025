using Bogus;
using FIAPCloudGames.Application.Services;
using FIAPCloudGames.Domain.Entities;
using FIAPCloudGames.Domain.Exceptions;
using FIAPCloudGames.Domain.Interfaces;
using NSubstitute;

namespace FIAPCloudGames.Tests.Application;

public class GameServiceTests
{
    private readonly IGameRepository _gameRepository;
    private readonly GameService _gameService;
    public GameServiceTests()
    {
        _gameRepository = Substitute.For<IGameRepository>();
        _gameService = new GameService(_gameRepository);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("  ")]
    public async Task Register_ShouldThrowException_WhenTitleIsNullOrWhiteSpace(string title)
    {
        var description = "A fun game to play with your friends.";
        var releaseDate = DateTime.Now;
        var price = 59.99m;

        await Assert.ThrowsAnyAsync<ArgumentException>(async () => await _gameService.RegisterAsync(title, description, releaseDate, price));
    }

    [Fact]
    public async Task Register_ShouldThrowException_WhenPriceNegative()
    {
        var title = "Game Title";
        var description = "A fun game to play with your friends.";
        var releaseDate = DateTime.Now;
        var price = new Faker().Random.Number(-10, -1);

        await Assert.ThrowsAsync<GameNegativePriceException>(async () => await _gameService.RegisterAsync(title, description, releaseDate, price));
    }

    [Fact]
    public async Task Register_ShouldReturnGame_WhenValid()
    {
        var title = "Game Title";
        var description = "A fun game to play with your friends.";
        var releaseDate = DateTime.Now;
        var price = new Faker().Random.Number(9999);

        var game = await _gameService.RegisterAsync(title, description, releaseDate, price);

        Assert.Equal(title, game.Title);
        Assert.Equal(description, game.Description);
        Assert.Equal(releaseDate, game.ReleaseDate);
        Assert.Equal(price, game.Price);
    }

    [Fact]
    public async Task GetAll_ShouldReturnAllGames()
    {
        const int QuantidadeGames = 3;
        var gameFaker = new Faker<Game>()
            .CustomInstantiator(f => Game.New(
                Guid.NewGuid(),
                f.Commerce.ProductName(),
                f.Lorem.Sentence(),
                f.Date.Recent(),
                f.Random.Decimal(10, 100)
            ));

        var games = gameFaker.Generate(QuantidadeGames);

        _gameRepository.GetAllAsync().Returns(games);

        var result = await _gameService.GetAllAsync();

        Assert.Equal(QuantidadeGames, result.Count());
    }


    [Fact]
    public async Task GetById_ShouldReturnNull_WhenIdNotExists()
    {
        var nonExistentId = Guid.NewGuid();
        _gameRepository.GetByIdAsync(nonExistentId).Returns((Game?)null);

        var result = await _gameService.GetByIdAsync(nonExistentId);

        Assert.Null(result);
    }

    [Fact]
    public async Task GetById_ShouldReturnGame_WhenIdExists()
    {
        var gameId = Guid.NewGuid();
        var gameFaker = new Faker<Game>()
            .CustomInstantiator(f => Game.New(
                gameId,
                f.Commerce.ProductName(),
                f.Lorem.Sentence(),
                f.Date.Recent(),
                f.Random.Decimal(10, 100)
            ));

        var game = gameFaker.Generate();
        _gameRepository.GetByIdAsync(gameId).Returns(game);

        var result = await _gameService.GetByIdAsync(gameId);

        Assert.NotNull(result);
        Assert.Equal(gameId, result.Id);
        Assert.Equal(game.Title, result.Title);
    }

    [Fact]
    public async Task Update_ShouldReturnNull_WhenIdNotExists()
    {
        var nonExistentId = Guid.NewGuid();
        var faker = new Faker();

        _gameRepository.GetByIdAsync(nonExistentId).Returns((Game?)null);

        var result = await _gameService.UpdateAsync(
            nonExistentId,
            faker.Commerce.ProductName(),
            faker.Lorem.Sentence(),
            faker.Date.Recent(),
            faker.Random.Decimal(10, 100)
        );

        Assert.Null(result);
    }


    [Fact]
    public async Task Update_ShouldUpdateOnlyInformedParameters_WhenParametersValid()
    {
        var gameId = Guid.NewGuid();
        var originalTitle = "Original Title";
        var originalDescription = "Original Description";
        var originalReleaseDate = DateTime.Now.AddDays(-100);
        var originalPrice = 49.99m;

        var game = Game.New(gameId, originalTitle, originalDescription, originalReleaseDate, originalPrice);
        _gameRepository.GetByIdAsync(gameId).Returns(game);

        var faker = new Faker();
        var newTitle = faker.Random.Bool() ? faker.Commerce.ProductName() : null;
        var newDescription = faker.Random.Bool() ? faker.Lorem.Sentence() : null;
        var newReleaseDate = faker.Random.Bool() ? faker.Date.Recent() : (DateTime?)null;
        var newPrice = faker.Random.Bool() ? faker.Random.Decimal(10, 100) : (decimal?)null;

        var result = await _gameService.UpdateAsync(gameId, newTitle, newDescription, newReleaseDate, newPrice);

        Assert.NotNull(result);
        Assert.Equal(newTitle ?? originalTitle, result.Title);
        Assert.Equal(newDescription ?? originalDescription, result.Description);
        Assert.Equal(newReleaseDate ?? originalReleaseDate, result.ReleaseDate);
        Assert.Equal(newPrice ?? originalPrice, result.Price);
        await _gameRepository.Received(1).UpdateAsync(game);
    }

    [Fact]
    public async Task Delete_ShouldReturnFalse_WhenIdNotExists()
    {
        var nonExistentId = Guid.NewGuid();
        _gameRepository.GetByIdAsync(nonExistentId).Returns((Game?)null);

        var result = await _gameService.DeleteAsync(nonExistentId);

        Assert.False(result);
        await _gameRepository.DidNotReceive().RemoveAsync(Arg.Any<Game>());
    }

    [Fact]
    public async Task Delete_ShouldDeleteGame()
    {
        var gameId = Guid.NewGuid();
        var gameFaker = new Faker<Game>()
            .CustomInstantiator(f => Game.New(
                gameId,
                f.Commerce.ProductName(),
                f.Lorem.Sentence(),
                f.Date.Recent(),
                f.Random.Decimal(10, 100)
            ));

        var game = gameFaker.Generate();
        _gameRepository.GetByIdAsync(gameId).Returns(game);

        var result = await _gameService.DeleteAsync(gameId);

        Assert.True(result);
        await _gameRepository.Received(1).RemoveAsync(game);
    }

}
