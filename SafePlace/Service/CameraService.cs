using SafePlace.DBCommunication;
using SafePlace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafePlace.Service
{
    public class CameraService : ICameraService
    {
        DBCommunicator _dBCommunicator = DBCommunicator.Instace;

        public Camera CreateCamera()
        {
            var camera = new Camera()
            {
                Guid = Guid.NewGuid(),
                //Test data for implementing ItemSource, which takes items from ObservableCollection<Person>
                //IdentifiedPeople = new List<Person>() {
                //    new Person() { Name = "John", LastName = "Johnson" },
                //    new Person() { Name = "John", LastName = "Seenhim" }
                //}
            };
            return camera;
        }
        //A more convenient method for creating camera instances for preview cameras during editing or creation of new cameras.
        public Camera CreateCamera(bool isCameraPermanent, int x = 0, int y = 0)
        {
            var camera = new Camera()
            {
                Guid = Guid.NewGuid(),
                //Test data for implementing ItemSource, which takes items from ObservableCollection<Person>
                IdentifiedPeople = new List<Person>() {
                    new Person() { Name = "Peter", LastName = "Peterson" },
                    new Person() { Name = "John", LastName = "Seenhim" }
                },
                PositionX = x,
                PositionY = y,
            };
            return camera;
        }

        public IEnumerable<Camera> GetAllCameras()
        {
            return _dBCommunicator.GetCameras();
        }

        public Camera GetCamera(Guid guid)
        {
            return DBCommunicator.Instace.GetCamera(guid);
        }
        //Method for removing cameras. Returns true if the camera was found.
        public void DeleteCamera(Camera camera)
        {
            _dBCommunicator.Delete(camera);
        }
        /// <summary>
        /// Don't use this unless camera does not have floor 
        /// When floor is added or updated cameras are also added by EF
        /// </summary>
        /// <param name="camera"></param>
        public void AddCamera(Camera camera)
        {
            _dBCommunicator.AddCamera(camera);
        }

        public bool RemoveCamera(Guid guid)
        {
            throw new NotImplementedException("Obsolete. If working wioth database use DeleteCamera(Camera camera), " +
                                            "otherwise use WPFCameraService");
        }

        public void Update(Camera camera)
        {
            _dBCommunicator.Update(camera);
        }
    }
}
