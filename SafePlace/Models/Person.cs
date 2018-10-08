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

        public Person(string name, string lastname)
        {
            this.Name = name;
            this.LastName = lastname;
            this.Authorization = 1; 
        }
    }
}
