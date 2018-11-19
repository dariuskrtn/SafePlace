using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
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
            return service.GetAllCameras();
        }

        // GET: api/Cameras/5
        public IHttpActionResult Get(Guid id)
        {
            var cameras = service.GetAllCameras();
            Camera cam = cameras.FirstOrDefault(camera => camera.Guid == id);
            if (cam == null) return NotFound();
            return Ok(cam);
        }

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
