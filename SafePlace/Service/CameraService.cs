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
                Transform = new System.Windows.Media.TransformGroup()
            };
            cameras.Add(camera.Guid, camera);
            return camera;
        }

        public Camera GetCamera(Guid guid)
        {
            if (cameras.ContainsKey(guid)) return cameras[guid];
            return null;
        }
    }
}
