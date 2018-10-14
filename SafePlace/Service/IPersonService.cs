using SafePlace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafePlace.Service
{
    interface IPersonService
    {
        Person GetPerson(Guid guid);
        Person CreatePerson();
    }
}
