using SafePlace.Models;
using SafePlace.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebService.Models;

namespace WebService.Controllers
{
    public class FloorsController : ApiController
    {
        IFloorService Service = new FloorService();
        ICameraService CameraService = new CameraService();
        // GET: api/Floors
        public IEnumerable<FloorDTO> Get()
        {
            return Service.GetFloorList().Select(floor => new FloorDTO(floor));
        }

        // GET: api/Floors/23005604-eb1b-11de-85ba-806d6172696f
        public IHttpActionResult Get(Guid id)
        {
            var floors = Service.GetFloorList();
            Floor floor = floors.FirstOrDefault(aFloor => aFloor.Guid == id);
            //If the floor is not in the DataBase, service returns error 404.
            if (floor == null) return NotFound();
            return Ok(floors);
        }
        
        // POST: api/Floors
        public void Post([FromBody]FloorDTO floorDTO)
        {
            Floor floor = GetFloorFromDTO(floorDTO);
            Service.Add(floor);
        }

        // PUT: api/Floors
        public void Put([FromBody]FloorDTO floorDTO)
        {
            Floor floor = GetFloorFromDTO(floorDTO);
            Service.Update(floor);
        }

        // DELETE: api/Floors/23005604-eb1b-11de-85ba-806d6172696f
        public void Delete(Guid id)
        {
            Service.Delete(Service.GetFloor(id));
        }

        //A helper function to get a floor from its DTO version.
        private Floor GetFloorFromDTO(FloorDTO floorDTO)
        {
            Floor floor = new Floor();
            //We copy attributes that aren't Guid or Guid collections.
            FloorDTO.GetAttributesFromDTO(floorDTO, floor);
            //For each Guid in a collection we get the apropriate camera.
            floor.Cameras = (ICollection<Camera>)floorDTO.Cameras.Select(cameraGuid => CameraService.GetCamera(cameraGuid));
            return floor;
        }
    }
}
