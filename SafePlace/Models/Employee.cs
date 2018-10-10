using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafePlace.Models
{
    class Employee : Person
    {
        //could add some information unique to employees
        public Employee(string name, string lastname) : base(name, lastname)
        {
            this.Authorization = 2;
        }
    }
}
