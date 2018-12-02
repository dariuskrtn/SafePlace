using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafePlace.DTO
{
    public class SpottedPeople
    {
        public Guid Camera { get; set; }
        public IEnumerable<SpottedPerson> spottedPeople { get; set; }
    }
}
