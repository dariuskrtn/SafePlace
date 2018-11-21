using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SafePlace.DBCommunication;
using SafePlace.Models;

namespace SafePlace.Service
{
    public class PersonService : IPersonService
    {
        DBCommunicator _dBCommunicator = DBCommunicator.Instace;

        public Person CreatePerson()
        {
            var person = new Person();
            person.Guid = Guid.NewGuid();
            return person;
        }

        public void AddPerson(Person person)
        {
            _dBCommunicator.AddPerson(person);
        }

        public Person GetPerson(Guid guid)
        {
            return _dBCommunicator.GetPerson(guid);
        }

        public IEnumerable<Person> GetPeople()
        {
            return _dBCommunicator.GetPeople();
        }
    }
}
