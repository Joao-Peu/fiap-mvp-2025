using Bogus;
using FIAPCloudGames.Domain.Entities;
using FIAPCloudGames.Domain.Exceptions;

namespace FIAPCloudGames.Tests.Domain;

public class GameTests
{
    [Fact]
    public async Task New_ShoulReturnNewGame()
    {
        var faker = new Faker();
        var id = Guid.NewGuid();
        var title = faker.Commerce.ProductName();
        var description = faker.Lorem.Sentence();
        var releaseDate = faker.Date.Recent();
        var price = faker.Random.Decimal(10, 100);

        var game = Game.New(id, title, description, releaseDate, price);

        Assert.NotNull(game);
        Assert.Equal(id, game.Id);
        Assert.Equal(title, game.Title);
        Assert.Equal(description, game.Description);
        Assert.Equal(releaseDate, game.ReleaseDate);
        Assert.Equal(price, game.Price);
    }

    [Fact]
    public async Task Update_ShouldThrowException_WhenNegativePrice()
    {
        var faker = new Faker();
        var game = Game.New(
            Guid.NewGuid(),
            faker.Commerce.ProductName(),
            faker.Lorem.Sentence(),
            faker.Date.Recent(),
            faker.Random.Decimal(10, 100)
        );

        var negativePrice = faker.Random.Decimal(-100, -1);

        Assert.Throws<GameNegativePriceException>(() => game.Update(null, null, null, negativePrice));
    }

    [Fact]
    public async Task Update_ShouldUpdateOnlyNonNullableProperties()
    {
        var faker = new Faker();
        var originalTitle = "Original Title";
        var originalDescription = "Original Description";
        var originalReleaseDate = DateTime.Now.AddDays(-100);
        var originalPrice = 49.99m;

        var game = Game.New(Guid.NewGuid(), originalTitle, originalDescription, originalReleaseDate, originalPrice);

        var newTitle = faker.Commerce.ProductName();
        var newPrice = faker.Random.Decimal(10, 100);

        game.Update(newTitle, null, null, newPrice);

        Assert.Equal(newTitle, game.Title);
        Assert.Equal(originalDescription, game.Description);
        Assert.Equal(originalReleaseDate, game.ReleaseDate);
        Assert.Equal(newPrice, game.Price);
    }
}
