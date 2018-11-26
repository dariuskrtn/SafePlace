using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebService.Controllers
{
    public class ImagesController : ApiController
    {
        //jei debug paleidi ir parašai: http://localhost:54507/api/images, gauni byte kodą nuotraukos iš App_Data.
        //GET: /api/images
        public string Get()
        {
            var root = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            var path = Path.Combine(root, "App_Data/when-you-study.jpg");

            var bytes = File.ReadAllBytes(path);
            var base64 = Convert.ToBase64String(bytes);

            return "data:image/jpeg;base64," + base64;
        }

        //GET: /api/images/?????????
        public string Get(string uri)
        {
            var root = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            //Uri = "App_Data/Koala.jpg" ar kitoks string, atitinkantis nuotrauka siame projekte.
            var path = Path.Combine(root, uri);

            var bytes = File.ReadAllBytes(path);
            var base64 = Convert.ToBase64String(bytes);

            return "data:image/jpeg;base64," + base64;
        }
    }
}
