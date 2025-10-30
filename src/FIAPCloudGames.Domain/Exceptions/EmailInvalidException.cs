using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAPCloudGames.Domain.Exceptions;

/// <summary>
/// Exception para indicar que o email informado está inválido.
/// </summary>
/// <inheritdoc cref="EmailInvalidException"/>
public class EmailInvalidException(string email) : Exception($"O email informado está inválido. {email}")
{
}
