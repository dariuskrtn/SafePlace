using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SafePlace.DB;
using SafePlace.Models;
using SafePlace.Service;
using WebService.Models;

namespace WebService.Controllers
{
    public class CamerasController : ApiController
    {
        ICameraService Service = new CameraService();
        IFloorService FloorService = new FloorService();
        IPersonService PersonService = new PersonService();

        // GET: api/Cameras
        public IHttpActionResult Get()
        {
            // Returns nothing as CameraService does not communicate with DB in this branch.
            return Ok(Service.GetAllCameras().Select(camera => new CameraDTO(camera)));
        }

        // GET: api/Cameras/23005604-eb1b-11de-85ba-806d6172696f
        public IHttpActionResult Get(Guid id)
        {
            var cameras = Service.GetAllCameras();
            Camera cam = cameras.FirstOrDefault(camera => camera.Guid == id);
            //If the camera is not in the DataBase, service returns error 404.
            if (cam == null) return NotFound();
            return Ok(cam);
        }
        
        // POST: api/Cameras
        public void Post([FromBody]CameraDTO camDTO)
        {
            Camera cam = CameraDTO.CameraFromDTO(camDTO);
            if (camDTO.Floor != null) cam.Floor = FloorService.GetFloor((Guid)camDTO.Floor);
            else cam.Floor = null;
            //If camDTO.People is null, this cycle breaks. camDTO.people shouldn't be null, it should be an empty data structure.
            if (camDTO.People != null) foreach(Guid personGuid in camDTO.People)
            {
                cam.People.Add(PersonService.GetPerson(personGuid));
            }
            //Seems like we need a person type service too.
            cam.PersonTypes = new List<PersonType>();
            Service.AddCamera(cam);
        }

        // PUT: api/Cameras/23005604-eb1b-11de-85ba-806d6172696f
        public void Put(int id, [FromBody]string value)
        {

        }

        // DELETE: api/Cameras/23005604-eb1b-11de-85ba-806d6172696f
        public void Delete(Guid id)
        {
            Service.DeleteCamera(Service.GetCamera(id));
        }
    }
}
