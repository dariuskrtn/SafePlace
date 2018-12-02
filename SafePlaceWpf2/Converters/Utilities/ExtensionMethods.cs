using SafePlace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafePlace.Utilities
{
    static class ExtensionMethods
    {

        public static Camera GetFloorCamera(this IList<Camera> cameraList, Guid guid)
        {

            foreach (Camera camera in cameraList)
            { 
                if (guid.Equals(camera.Guid))
                    return camera;
            }
            return null;
        }

        public static Camera GetFloorCamera(this ICollection<Camera> cameraList, Guid guid)
        {

            foreach (Camera camera in cameraList)
            {
                if (guid.Equals(camera.Guid))
                    return camera;
            }
            return null;
        }
    }
}
