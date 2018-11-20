using SafePlace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{
    [Serializable]
    public class PersonDTO
    {
        public Guid Guid { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public PersonDTO(Person person)
        {
            Guid = person.Guid;
            Name = person.Name;
            LastName = person.LastName;
        }
    }
}