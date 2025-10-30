namespace FIAPCloudGames.Domain.Exceptions;

/// <inheritdoc cref="EntityNotFoundException"/>
public class EntityNotFoundException(Guid id, string entityName) : Exception($"{entityName} não foi encontrada para o Id '{id}'.")
{
}
