﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SafePlace.Models;
using SafePlace.Service;

namespace WebService.Controllers
{
    public class PeopleController : ApiController
    {
        IPersonService service = new PersonService();
        // GET: api/Cameras
        public IEnumerable<Person> Get()
        {
            // Returns nothing as CameraService does not communicate with DB in this branch.
            return service.GetPeople();
        }

        // GET: api/Cameras/23005604-eb1b-11de-85ba-806d6172696f
        public IHttpActionResult Get(Guid id)
        {
            var people = service.GetPeople();
            Person person = people.FirstOrDefault(aPerson => aPerson.Guid == id);
            //If the person is not in the DataBase, service returns error 404.
            if (person == null) return NotFound();
            return Ok(person);
        }

        //To implement?:

        // POST: api/People
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/People/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/People/5
        public void Delete(int id)
        {
        }
    }
}
