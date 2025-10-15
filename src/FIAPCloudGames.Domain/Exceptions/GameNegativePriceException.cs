using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAPCloudGames.Domain.Exceptions;

/// <summary>
/// Exception para indicar que o preço de um jogo é negativo.
/// </summary>
public class GameNegativePriceException : Exception
{
    /// <inheritdoc cref="GameNegativePriceException"/>
    public GameNegativePriceException() : base("O preço do jogo não pode ser negativo.")
    {
    }
}
