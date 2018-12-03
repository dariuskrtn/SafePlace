using SafePlace.Service;
using SafePlaceFaceRecognition;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using WebService.DependencyHandling;

namespace WebService.Controllers
{
    public class ImagesController : ApiController
    {
        IImageService ImageService = DependencyHandler.ImageService.Value;

        public HttpResponseMessage Get(Guid id)
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(ImageService.GetImage(id));
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
            return result;
        }

        //The floor image should be in the body as contents of the request.
        //The request's contents should be a MIME containing the image of the floor.
        //MIME - Multipurpose Internet Mail Extensions
        //If the image was added successfully, the response contains guid of the saved image.
        public HttpResponseMessage Post()
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            Guid FloorImageGuid = Guid.Empty;
            if (Request.Content.IsMimeMultipartContent())
            {
                Request.Content.ReadAsMultipartAsync<MultipartMemoryStreamProvider>(new MultipartMemoryStreamProvider()).ContinueWith((task) =>
                {
                    MultipartMemoryStreamProvider provider = task.Result;
                    foreach (HttpContent content in provider.Contents)
                    {
                        Stream stream = content.ReadAsStreamAsync().Result;
                        FloorImageGuid = ImageService.SaveImage(stream);
                    }
                }).Wait();
                
                result.Content = new StringContent(FloorImageGuid.ToString());
                return result;
            }
            else
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted"));
            }
        }

        //The request's contents should be a collection of face images in a MIME format.
        [HttpPost]
        [Route("api/images/person/{personGuid}")]
        public HttpResponseMessage PostFaceImages(Guid personGuid)
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            var faces = new List<Guid>();
            var faceImages = new List<Bitmap>();
            if (Request.Content.IsMimeMultipartContent())
            {
                Request.Content.ReadAsMultipartAsync<MultipartMemoryStreamProvider>(new MultipartMemoryStreamProvider()).ContinueWith((task) =>
                {
                    MultipartMemoryStreamProvider provider = task.Result;
                    foreach (HttpContent content in provider.Contents)
                    {
                        Stream stream = content.ReadAsStreamAsync().Result;
                        faceImages.Add(Image.FromStream(stream) as Bitmap);
                        faces.Add(ImageService.SaveImage(stream));
                    }
                });
                //Face images are saved now. Should azure face registration start here?
                //The ids of all faces are stored in a list and all images can be retrieved.
                FaceRecognition.GetInstance().GetFaceRecognitionService().RegisterPerson(personGuid.ToString(), faceImages);
                //To do figure out a way to save face images so that you can reach them provided you have the GUID of the person.
                //Might need separate get and save functions for facial images.
                return result;
            }
            else
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted"));
            }
        }
    }
}