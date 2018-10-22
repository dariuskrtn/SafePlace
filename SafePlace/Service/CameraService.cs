using SafePlace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafePlace.Service
{
    class CameraService : ICameraService
    {
        private Dictionary<Guid, Camera> cameras = new Dictionary<Guid, Camera>();

        public Camera CreateCamera()
        {
            var camera = new Camera()
            {
                Guid = Guid.NewGuid(),
                //Test data for implementing ItemSource, which takes items from ObservableCollection<Person>
                IdentifiedPeople = new List<Person>() {
                    new Person() { Name = "John", LastName = "Johnson" },
                    new Person() { Name = "John", LastName = "Seenhim" }
                }
            };
            cameras.Add(camera.Guid, camera);
            return camera;
        }
        //A more convenient method for creating camera instances for preview cameras during editing or creation of new cameras.
        public Camera CreateCamera(int x, int y, bool createCamera)
        {
            var camera = new Camera()
            {
                Guid = Guid.NewGuid(),
                //Test data for implementing ItemSource, which takes items from ObservableCollection<Person>
                IdentifiedPeople = new List<Person>() {
                    new Person() { Name = "John", LastName = "Johnson" },
                    new Person() { Name = "John", LastName = "Seenhim" }
                },
                PositionX = x,
                PositionY = y,
            };
            if (createCamera) cameras.Add(camera.Guid, camera);
            return camera;
        }

        public IEnumerable<Camera> GetAllCameras()
        {
            return cameras.Select(item => item.Value);
        }

        public Camera GetCamera(Guid guid)
        {
            if (cameras.ContainsKey(guid)) return cameras[guid];
            return null;
        }
        //Method for removing cameras. Returns true if the camera was found.
        public bool RemoveCamera(Guid guid)
        {
            return cameras.Remove(guid);
        }
    }
}
