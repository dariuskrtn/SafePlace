using SafePlace.DBCommunication;
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
    public class FloorService : IFloorService
    {
        DBCommunicator _dBCommunicator = DBCommunicator.Instace;

        public void Add(Floor floor)
        {
            if (null == floor)
                return;
            _dBCommunicator.AddFloor(floor);
        }

        public Floor CreateFloor()
        {
            return CreateFloor("/Images/Floor.png");
        }
        
        public Floor CreateFloor(string Path)
        {
            var floor = new Floor();
            floor.Guid = Guid.NewGuid();
            floor.Cameras = new List<Camera>();
            floor.ImagePath = Path;
            var img = new BitmapImage(new Uri(Path, UriKind.Relative));
            floor.FloorMap = img;
            return floor;
        }

        public Floor CreateFloor(string path, string floorName)
        {
            var floor = CreateFloor(path);
            floor.Name = floorName;
            return floor;
        }

        public Floor CreateEmptyFloor(string name)
        {
            return CreateFloor("/Images/no_image_icon.png", name);
        }

        public Floor GetFloor(Guid guid)
        {
            return _dBCommunicator.GetFloor(guid);
        }

        public IEnumerable<Floor> GetFloorList()
        {
            var floors = _dBCommunicator.GetFloors();
            foreach(var floor in floors)
            {
                if (floor.ImagePath != null) floor.FloorMap = new BitmapImage(new Uri(floor.ImagePath, UriKind.Relative));
                else floor.FloorMap = new BitmapImage(new Uri("/Images/Placeholder.png", UriKind.Relative));
            }
            return floors;
        }
    }
}
