using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class PersonNotFoundExcepiton : Exception
    {
        public PersonNotFoundExcepiton() { }

        public PersonNotFoundExcepiton(string message) : base(message) { }
    }
}
