using System.Collections.Generic;
using System.Text.RegularExpressions;
using FIAPCloudGames.Domain.ValueObject;

namespace FIAPCloudGames.Domain.Entities;

public class LibraryGame
{
    public Guid Id { get; private set; }
    public Library Library { get; private set; }
    public Game Game { get; private set; }

    public LibraryGame(Library library, Game game)
    {
        Library = library;
        Game = game;
    }
}
