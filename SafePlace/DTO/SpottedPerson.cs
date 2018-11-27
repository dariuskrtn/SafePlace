using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafePlace.DTO
{
    public class SpottedPerson
    {
        public Guid Camera { get; set; }
        public Guid Person { get; set; }

        public SpottedPerson(Guid cam, Guid person)
        {
            Camera = cam;
            Person = person;
        }
    }
}
