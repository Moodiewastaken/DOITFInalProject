using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBank.Exceptions
{
    internal class InvalidName : Exception
    {
        public InvalidName() : base("Please enter your full name") { }
    }
}
