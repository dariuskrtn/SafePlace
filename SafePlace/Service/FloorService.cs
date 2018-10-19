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
        // const naming?
        private const string DefaultFloorName = "Input floor name here";
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
            floor.FloorName = DefaultFloorName;

            floors.Add(floor.Guid, floor);
            return floor;
        }

        public Floor CreateEmptyFloor()
        {
            return CreateFloor("/Images/no_image_icon.png");
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
