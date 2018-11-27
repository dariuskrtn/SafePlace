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
        IFloorService service = new FloorService();
        // GET: api/Floors
        public IEnumerable<FloorDTO> Get()
        {
            return service.GetFloorList().Select(floor => new FloorDTO(floor));
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
        
        // POST: api/Floors
        public void Post([FromBody]Floor floor)
        {
            
            service.Add(floor);
        }

        // PUT: api/Floors/23005604-eb1b-11de-85ba-806d6172696f
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Floors/23005604-eb1b-11de-85ba-806d6172696f
        public void Delete(Guid id)
        {
            service.Delete(service.GetFloor(id));
        }
    }
}
