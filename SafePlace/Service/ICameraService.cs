using SafePlace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafePlace.Service
{
    public interface ICameraService
    {
        Camera GetCamera(Guid guid);
        IEnumerable<Camera> GetAllCameras();
        Camera CreateCamera();
        bool RemoveCamera(Guid guid);
        Camera CreateCamera(bool isCameraPermanent, int x = 0, int y = 0);
        void AddCamera(Camera camera);
    }
}
