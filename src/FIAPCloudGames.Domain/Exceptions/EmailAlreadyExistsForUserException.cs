using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAPCloudGames.Domain.Exceptions;

/// <summary>
/// Exception para indicar que o email informado está inválido.
/// </summary>
/// <inheritdoc cref="EmailAlreadyExistsForUserException"/>
public class EmailAlreadyExistsForUserException() : Exception($"Já existe um usuário ativo para o e-mail informado.")
{
}
