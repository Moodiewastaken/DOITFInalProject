using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman.Exceptions
{
    internal class InvalidGuess : Exception
    {
        public InvalidGuess() : base("Please enter a letter.") { }
    }
}
