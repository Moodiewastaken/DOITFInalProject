using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessingGame.Exceptions
{
    internal class InvalidDifficultyException : Exception
    {
        public InvalidDifficultyException() : base("Invalid Input!") { }
    }
}
