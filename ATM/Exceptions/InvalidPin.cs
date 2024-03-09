using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBank.Exceptions
{
    internal class InvalidPin : Exception
    {
        public InvalidPin() : base("Invalid pin, it should be 11 characters long" ) { }
    }
}
