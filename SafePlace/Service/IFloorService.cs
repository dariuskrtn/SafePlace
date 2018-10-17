using SafePlace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafePlace.Service
{
    interface IFloorService
    {
        Floor GetFloor(Guid guid);
        Floor CreateFloor();
        Floor CreateFloor(string path);
        IEnumerable<Floor> GetFloorList();
    }
}
