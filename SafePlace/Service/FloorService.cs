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
            return CreateFloor("/Images/Floor.png");
        }
        
        public Floor CreateFloor(string Path)
        {
            var floor = new Floor();
            floor.Guid = Guid.NewGuid();
            floor.Cameras = new List<Camera>();

            var img = new BitmapImage(new Uri(Path, UriKind.Relative));
            floor.FloorMap = img;

            floors.Add(floor.Guid, floor);
            return floor;
        }

        public Floor CreateFloor(string path, string floorName)
        {
            var floor = CreateFloor(path);
            floor.FloorName = floorName;
            return floor;
        }

        public Floor CreateEmptyFloor(string name)
        {
            return CreateFloor("/Images/no_image_icon.png", name);
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
