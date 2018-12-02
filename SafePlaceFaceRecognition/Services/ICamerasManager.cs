using SafePlace.DTO;
using SafePlace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafePlaceFaceRecognition.Service
{
    public interface ICamerasManager
    {
        IEnumerable<SpottedPerson> GetSpottedPeople();
        void AddCamera(Camera camera);
        void AddCameras(IEnumerable<Camera> cameras);

    }
}
