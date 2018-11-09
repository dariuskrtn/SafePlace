using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafePlace.Service
{
    public interface IDatabaseService
    {
        void AddPerson(Guid guid, string name, string lastName, Guid personType = new Guid());
    }
}
