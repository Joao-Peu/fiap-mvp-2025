using FIAPCloudGames.Domain.Exceptions;
using System.Diagnostics;

namespace FIAPCloudGames.Domain.Entities
{
    public class Game
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTime ReleaseDate { get; private set; }
        public decimal Price { get; private set; }

        private Game(Guid id, string title, string description, DateTime releaseDate, decimal price)
        {
            Id = id;
            Title = title;
            Description = description;
            ReleaseDate = releaseDate;
            Price = price;
        }

        public static Game New(Guid id,string title, string description, DateTime releaseDate, decimal price)
        {
            return new Game(id, title, description, releaseDate, price);
        }

        public void Update(string? title, string? description, DateTime? releaseDate, decimal? price)
        {
            if (!string.IsNullOrWhiteSpace(title))
            {
                Title = title;
            }

            if (!string.IsNullOrWhiteSpace(description))
            {
                Description = description;
            }

            if(releaseDate.HasValue)
            {
                ReleaseDate = releaseDate.Value;
            }

            if (price.HasValue)
            {
                if (price < 0)
                {
                    throw new GameNegativePriceException();
                }

                Price = price.Value;
            }
        }
    }
}
