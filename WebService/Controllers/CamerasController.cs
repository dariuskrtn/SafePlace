using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SafePlace.DB;
using SafePlace.Models;
using SafePlace.Service;
namespace WebService.Controllers
{
    public class CamerasController : ApiController
    {
        ICameraService service = new CameraService();
        // GET: api/Cameras
        public IEnumerable<Camera> Get()
        {
            // Returns nothing as CameraService does not communicate with DB in this branch.
            return service.GetAllCameras();
        }

        // GET: api/Cameras/23005604-eb1b-11de-85ba-806d6172696f
        public IHttpActionResult Get(Guid id)
        {
            var cameras = service.GetAllCameras();
            Camera cam = cameras.FirstOrDefault(camera => camera.Guid == id);
            //If the camera is not in the DataBase, service returns error 404.
            if (cam == null) return NotFound();
            return Ok(cam);
        }

        //To be implemented?:

        // POST: api/Cameras
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Cameras/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Cameras/5
        public void Delete(int id)
        {
        }
    }
}
