using SafePlace.DTO;
using SafePlaceFaceRecognition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebService.Controllers
{
    public class PersonRecognitionController : ApiController
    {
        public IEnumerable<SpottedPerson> Get()
        {
            return FaceRecognition.GetInstance().GetCamerasManager().GetSpottedPeople();
        }
    }
}
