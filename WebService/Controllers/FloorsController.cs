using SafePlace.Models;
using SafePlace.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebService.Controllers
{
    public class FloorsController : ApiController
    {
        IFloorService service = new FloorService();
        // GET: api/Floors
        public IEnumerable<Floor> Get()
        {
            return service.GetFloorList();
        }

        // GET: api/Floors/23005604-eb1b-11de-85ba-806d6172696f
        public IHttpActionResult Get(Guid id)
        {
            var floors = service.GetFloorList();
            Floor floor = floors.FirstOrDefault(aFloor => aFloor.Guid == id);
            //If the floor is not in the DataBase, service returns error 404.
            if (floor == null) return NotFound();
            return Ok(floors);
        }

        //To be implemented?:

        // POST: api/Floors
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Floors/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Floors/5
        public void Delete(int id)
        {
        }
    }
}
