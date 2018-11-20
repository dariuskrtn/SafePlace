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

        public IEnumerable<CameraDTO> Cameras { get; set; }


        public FloorDTO(Floor floor)
        {
            Guid = floor.Guid;
            ImagePath = floor.ImagePath;
            Name = floor.Name;
            Cameras = floor.Cameras.Select(cam => new CameraDTO(cam));
        }
    }
}