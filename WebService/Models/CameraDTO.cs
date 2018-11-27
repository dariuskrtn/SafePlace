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

        //The following two were commented as they cause an error when the request GET api/floors is resolved. 
        //Perhaps the select doesn't include people and person types.
        //public IEnumerable<Guid> People { set; get; }

        public IEnumerable<Guid> PersonTypes { set; get; }
        public CameraDTO()
        {

        }
        public CameraDTO(Camera cam)
        {
            Guid = cam.Guid;
            IPAddress = cam.IPAddress;
            Name = cam.Name;
            PositionX = cam.PositionX;
            PositionY = cam.PositionY;
            Floor = cam.Floor?.Guid;
            //People = cam.People.Select(person => person.Guid);  //Not sure why but this might not work without a casting.
            PersonTypes = (IEnumerable<Guid>) cam.PersonTypes.Select(type => type.Guid);
        }

        //Copies DTO attributes to the database object.
        public static Camera CameraFromDTO(CameraDTO camDTO)
        {
            Camera camera = new Camera()
            {
                Guid = camDTO.Guid,
                IPAddress = camDTO.IPAddress,
                Name = camDTO.Name,
                PositionX = camDTO.PositionX,
                PositionY = camDTO.PositionY,
                //People = new List<Person>(),
                //PersonTypes = new List<PersonType>()
            };
            return camera;
        }
        public static void GetAttributesFromDTO(CameraDTO camDTO, Camera cam)
        {
            cam.Guid = camDTO.Guid;
            cam.IPAddress = camDTO.IPAddress;
            cam.Name = camDTO.Name;
            cam.PositionX = camDTO.PositionX;
            cam.PositionY = camDTO.PositionY;
        }
    }
}