using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAPCloudGames.Application.Exceptions
{
    public class DuplicateLibraryException : Exception
    {
        public DuplicateLibraryException() : base($"Já existe uma biblioteca criada para o usuário.")
        {
        }
    }
}
