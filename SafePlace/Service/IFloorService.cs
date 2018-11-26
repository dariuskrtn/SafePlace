using SafePlace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafePlace.Service
{
    public interface IFloorService
    {
        event EventHandler<UpdateFloorEventArgs> OnFloorListUpdated;

        void Add(Floor floor);
        Floor GetFloor(Guid guid);
        Floor CreateFloor();
        Floor CreateEmptyFloor(string name);
        Floor CreateFloor(string path);
        void Delete(Floor floor);
        IEnumerable<Floor> GetFloorList();
        void Update(Floor floor);
    }
}
