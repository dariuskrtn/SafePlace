using SafePlace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafePlace.Service
{
    interface ICameraService
    {
        Camera GetCamera(Guid guid);
        IEnumerable<Camera> GetAllCameras();
        Camera CreateCamera();
        bool RemoveCamera(Guid guid);
        Camera CreateCamera(int x, int y, bool isCameraPermanent);
        void AddCamera(Camera camera);
    }
}
