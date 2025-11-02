using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAPCloudGames.Application.Exceptions
{
    public class DuplicateGameInLibraryException : Exception
    {
        public DuplicateGameInLibraryException() : base("O jogo já existe na biblioteca do usuário.")
        {
        }
    }
}
