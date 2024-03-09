using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Exceptions
{
    internal class InvalidInputException : Exception
    {
        public InvalidInputException() : base("Please enter valid numbers") { }

    }
}
