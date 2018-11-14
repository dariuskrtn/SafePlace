using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafePlace.Models
{
    public partial class Person
    {
        #region DB part contains
        //public String Name { get; set; }
        //public String LastName { get; set; }
        //public ICollection<Guid> AllowedCameras { get; set; }
        //public Guid Guid { get; set; }
        //Possible types: Employee (Various team roles + intern), Guest, BusinessCollaborator (visitors from foreign branches or related companies).
        //Could be in format: type, subtype. For example: "Visitor, business partner" or "Employee, intern".
        //public String PersonType { get; set; }
        #endregion

        //Whenever a camera notices a person, following should happen:
        //1. If camera was not null, the person is removed from the camera's SpottedPeople list;
        //2. A new value is set and the person is added there.
        [NotMapped]
        public Camera Camera { set; get; }
        public Person() { }
        public Person(string name, string lastname)
        {
            this.Name = name;
            this.LastName = lastname;
        }

        override public string ToString()
        {
            return $"{Name} {LastName}";
        }

       
    }
}
