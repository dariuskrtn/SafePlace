﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafePlace.Models
{
    class Person
    {
        public String Name { get; set; }
        public String LastName { get; set; }
        public IList<Guid> AllowedCameras { get; set; }
        public Guid guid { get; set; }
        
        //Whenever a camera notices a person, following should happen:
        //1. If camera was not null, the person is removed from the camera's SpottedPeople list;
        //2. A new value is set and the person is added there.
        public Camera Camera { set; get; }
        public Person() { }
        public Person(string name, string lastname)
        {
            this.Name = name;
            this.LastName = lastname;
        }

       
    }
}
