using System.Collections.Generic;
using System.Text.RegularExpressions;
using FIAPCloudGames.Domain.ValueObject;

namespace FIAPCloudGames.Domain.Entities;

public class LibraryGame
{
    public Guid Id { get; private set; }
    public Library Library { get; private set; }
    public Game Game { get; private set; }
    public DateTime AcquiredAt { get; private set; }

    private LibraryGame()
    {

    }

    public LibraryGame(Library library, Game game)
    {
        Id = Guid.NewGuid();
        Library = library;
        Game = game;
        AcquiredAt = DateTime.UtcNow;
    }
}
