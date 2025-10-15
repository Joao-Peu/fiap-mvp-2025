using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAPCloudGames.Domain.Exceptions;

/// <summary>
/// Exception para indicar que o email informado está inválido.
/// </summary>
public class EmailInvalidException : Exception
{
    /// <inheritdoc cref="EmailInvalidException"/>
    public EmailInvalidException(string email) : base($"O email informado está inválido. {email}")
    {
    }
}
