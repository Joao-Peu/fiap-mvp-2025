using FIAPCloudGames.Domain.ValueObject;

namespace FIAPCloudGames.Domain.Entities;

public class Library
{
    public Guid Id { get; private set; }
    public bool IsActive { get; private set; }
    public User User { get; private set; }
    public ICollection<LibraryGame> OwnedGames { get; private set; } = [];

    private Library()
    {
        
    }

    public Library(User user)
    {
        Id = Guid.NewGuid();
        User = user;
        IsActive = true;
        OwnedGames = [];
    }

    public void Delete()
    {
        IsActive = false;
    }

    public void AddAcquiredGame(LibraryGame libraryGame)
    {
        // Verificar se o jogo já existe na biblioteca
        if (OwnedGames.Any(og => og.Game.Id == libraryGame.Game.Id))
        {
            return; // Jogo já existe, não adicionar duplicata
        }

        OwnedGames.Add(libraryGame);
    }

    public void RemoveAcquiredGame(LibraryGame libraryGame)
    {
        OwnedGames.Remove(libraryGame);
    }
}
