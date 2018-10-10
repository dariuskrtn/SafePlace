using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafePlace.Models
{
    class Person
    {
        public String Name;
        public String LastName;
        public int Authorization; //maybe we could use an enum for authorization
        //Various other information shall be added if necessary
        
        //Whenever a camera notices a person, following should happen:
        //1. If camera was not null, the person is removed from the camera's SpottedPeople list;
        //2. A new value is set and the person is added there.
        public Camera Camera { set; get; }
        public Person(string name, string lastname)
        {
            this.Name = name;
            this.LastName = lastname;
            this.Authorization = 1; 
        }

       
    }
}
