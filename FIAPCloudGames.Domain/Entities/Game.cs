namespace FIAPCloudGames.Domain.Entities
{
    public class Game
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTime ReleaseDate { get; private set; }
        public decimal Price { get; private set; }

        public Game(string title, string description, DateTime releaseDate, decimal price)
        {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            ReleaseDate = releaseDate;
            Price = price;
        }
    }
}
