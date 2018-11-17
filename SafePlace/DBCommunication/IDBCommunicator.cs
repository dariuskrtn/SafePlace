using SafePlace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafePlace.DBCommunication
{
    public interface IDBCommunicator
    {
        void AddPerson(Person person);
        void AddCamera(Camera camera);
        void AddFloor(Floor foor);
        void AddPersonType(PersonType personType);

        IDictionary<Guid, Person> GetPeople();
        IDictionary<Guid, Camera> GetCameras();
        IDictionary<Guid, Floor> GetFloors();
        IDictionary<Guid, PersonType> GetPersonTypes();

        void Update<T>(T model) where T : Model;
    }
}
