using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SafePlace.Models;
using SafePlace.Service;
using WebService.Models;

namespace WebService.Controllers
{
    public class PeopleController : ApiController
    {
        IPersonService service = new PersonService();
        ICameraService CameraService = new CameraService();
        // GET: api/Cameras
        public IEnumerable<PersonDTO> Get()
        {
            var dbPeople = service.GetPeople();
            return dbPeople.Select(person => new PersonDTO(person));
        }

        // GET: api/Cameras/23005604-eb1b-11de-85ba-806d6172696f
        public IHttpActionResult Get(Guid id)
        {
            var people = service.GetPeople();
            Person person = people.FirstOrDefault(aPerson => aPerson.Guid == id);
            //If the person is not in the DataBase, service returns error 404.
            if (person == null) return NotFound();
            return Ok(new PersonDTO(person));
        }
        
        // POST: api/People
        public IHttpActionResult Post([FromBody]PersonDTO personDTO)
        {
            Person person = GetPersonFromDTO(personDTO);
            service.AddPerson(person);
            //Some logic to differ whether the person was actually added.
            //If a database exception arises, should return NotOK() or AddFailed().
            return Ok(person.Guid);
        }

        // PUT: api/People/23005604-eb1b-11de-85ba-806d6172696f
        public void Put([FromBody]PersonDTO personDTO)
        {
            Person person = GetPersonFromDTO(personDTO);
            service.Update(person);
        }

        // DELETE: api/People/23005604-eb1b-11de-85ba-806d6172696f
        public void Delete(Guid id)
        {
            service.Delete(service.GetPerson(id));
        }
        
        private Person GetPersonFromDTO(PersonDTO personDTO)
        {
            Person person = new Person();
            PersonDTO.GetAttributesFromDTO(personDTO, person);
            person.AllowedCameras = personDTO.AllowedCameras.Select(cameraGuid => CameraService.GetCamera(cameraGuid)).ToList();
            return person;
        }
    }
}
