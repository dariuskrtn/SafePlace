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
        //Should the "Guid" be "Guid?" instead, some people might not have a person type, unless there is a default person type.
        public Guid? PersonType { get; set; }

        public virtual IEnumerable<Guid> AllowedCameras { get; set; }

        public PersonDTO(Person person)
        {
            Guid = person.Guid;
            Name = person.Name;
            LastName = person.LastName;
            PersonType = person.PersonType?.Guid;
            //The following does not work well with DB for some reason. Thus I commented it out.
            //AllowedCameras = person.AllowedCameras.Select(allowedCamera => allowedCamera.Guid);
            AllowedCameras = new List<Guid>();
        }
    }
}