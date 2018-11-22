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
        // Obsolete, may be used wioth wpf app without DB
        bool RemoveCamera(Guid guid);
        // Deletes in DB
        void DeleteCamera(Camera camera);
        Camera CreateCamera(bool isCameraPermanent, int x = 0, int y = 0);
        void AddCamera(Camera camera);
    }
}
