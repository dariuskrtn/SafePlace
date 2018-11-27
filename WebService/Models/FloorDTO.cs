using SafePlace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{
    [Serializable]
    public class FloorDTO
    {
        public Guid Guid { get; set; }

        public string ImagePath { get; set; }

        public string Name { get; set; }

        public IEnumerable<Guid> Cameras { get; set; }

        //For some reason [FromBody] takes the default initializer of a class to create an object. 
        //This is here so that the constructor with floor is not used. It breaks if floor is null, as floor.Guid is not solveable.
        public FloorDTO()
        {

        }

        public FloorDTO(Floor floor)
        {
            Guid = floor.Guid;
            ImagePath = floor.ImagePath;
            Name = floor.Name;
            Cameras = floor.Cameras.Select(cam => cam.Guid);
        }

        public static void GetAttributesFromDTO(FloorDTO floorDTO, Floor floor)
        {
            floor.Guid = floorDTO.Guid;
            floor.ImagePath = floorDTO.ImagePath;
            floor.Name = floorDTO.Name;
            //Would have really liked to have a cameras service here.
            //floor.Cameras = floorDTO.Cameras.Select(camGuid => camerasService.GetCamera(cam));
        }
    }
}