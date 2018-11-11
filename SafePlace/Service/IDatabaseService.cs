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
        void AddPerson(Guid guid, string name, string lastName, ICollection<Guid> allowedCameras, Guid personType = new Guid());
        void AddCamera(Guid guid, string iPAdress, string name, int posX, int posY, Guid floor);
        void AddFloor(Guid guid, string imagePath, string name);
        void AddPersonType(Guid guid, string name, ICollection<Guid> allowedCameras = null);
        void AddPersonTypeCameras(Guid guid, ICollection<Guid> allowedCameras);
    }
}
