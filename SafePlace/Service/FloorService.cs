using SafePlace.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SafePlace.Service
{
    class FloorService : IFloorService
    {
        private Dictionary<Guid, Floor> floors = new Dictionary<Guid, Floor>();

        public Floor CreateFloor()
        {
            var floor = new Floor();
            floor.guid = Guid.NewGuid();

            var img = new BitmapImage(new Uri("/Images/Floor.png", UriKind.Relative));
            floor.FloorMap = img;

            floors.Add(floor.guid, floor);
            return floor;
        }

        public Floor GetFloor(Guid guid)
        {
            if (floors.ContainsKey(guid)) return floors[guid];
            return null;
        }

        public IEnumerable<Floor> GetFloorList()
        {
            return floors.Select(item => item.Value);
        }
    }
}
