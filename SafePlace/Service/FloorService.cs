﻿using SafePlace.DBCommunication;
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
    public class UpdateFloorEventArgs : EventArgs
    {
        public  Floor _floor;

        public UpdateFloorEventArgs(Floor floor)
        {
            _floor = floor;
        }
    }


    public class FloorService : IFloorService
    {
        public event EventHandler<UpdateFloorEventArgs> OnFloorListUpdated;

        DBCommunicator _dBCommunicator = DBCommunicator.Instace;

        public void Add(Floor floor)
        {
            if (null == floor)
                return;
            _dBCommunicator.AddFloor(floor);
            OnFloorListUpdated(this, new UpdateFloorEventArgs(floor));
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

        public void Update(Floor floor)
        {
            _dBCommunicator.Update(floor);
            //This throws an exception and a floor is still updated.
            OnFloorListUpdated(this, new UpdateFloorEventArgs(floor));
        }

        public Floor GetFloor(Guid guid)
        {
            return _dBCommunicator.GetFloor(guid);
        }

        public IEnumerable<Floor> GetFloorList()
        {
            var floors = _dBCommunicator.GetFloors();
            if (floors == null)
                return null;
            foreach(var floor in floors)
            {
                try
                {
                    if (!floor.ImagePath.StartsWith("/Images/"))
                        floor.FloorMap = new BitmapImage(new Uri(floor.ImagePath, UriKind.Absolute));
                    else if (floor.ImagePath != null)
                        floor.FloorMap = new BitmapImage(new Uri(floor.ImagePath, UriKind.Relative));
                    else
                        floor.FloorMap = new BitmapImage(new Uri("/Images/Placeholder.png", UriKind.Relative));
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return floors;
        }

        public void Delete(Floor floor)
        {
            _dBCommunicator.Delete(floor);
            OnFloorListUpdated(this, new UpdateFloorEventArgs(floor));
        }
    }
}
