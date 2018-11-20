using SafePlace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{
    [Serializable]
    public class CameraDTO
    {
        public Guid Guid { get; set; }

        public string IPAddress { get; set; }

        public string Name { get; set; }

        public int PositionX { get; set; }

        public int PositionY { get; set; }

        public Guid? Floor { get; set; }

        public ICollection<Person> People { set; get; }

        public ICollection<PersonType> PersonTypes { set; get; }

        public CameraDTO(Camera cam)
        {
            Guid = cam.Guid;
            IPAddress = cam.IPAddress;
            Name = cam.Name;
            PositionX = cam.PositionX;
            PositionY = cam.PositionY;
            Floor = cam.Floor?.Guid;
            People = cam.People;
            PersonTypes = cam.PersonTypes;
        }
    }
}