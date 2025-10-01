using FIAPCloudGames.Domain.ValueObject;

namespace FIAPCloudGames.Domain.Entities;

public class Library
{
    public Guid Id { get; private set; }
    public bool IsDeleted { get; private set; }
    public User User { get; private set; }
    public ICollection<LibraryGame> OwnedGames { get; private set; } = [];

    public Library(User user)
    {
        User = user;
        OwnedGames = [];
    }

    public void Delete()
    {
        IsDeleted = true;
    }

    public void AddAcquiredGame(LibraryGame libraryGame)
    {
        if (OwnedGames.Contains(libraryGame))
        {
            return;
        }

        OwnedGames.Add(libraryGame);
    }

    public void RemoveAcquiredGame(LibraryGame libraryGame)
    {
        OwnedGames.Remove(libraryGame);
    }
}
