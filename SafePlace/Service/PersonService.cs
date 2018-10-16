using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SafePlace.Models;

namespace SafePlace.Service
{
    class PersonService : IPersonService
    {
        private Dictionary<Guid, Person> people = new Dictionary<Guid, Person>();

        public Person CreatePerson()
        {
            var person = new Person();
            person.Guid = Guid.NewGuid();
            people.Add(person.Guid, person);
            return person;
        }

        public Person GetPerson(Guid guid)
        {
            if (people.ContainsKey(guid)) return people[guid];
            return null;
            
        }
    }
}
