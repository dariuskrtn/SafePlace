using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SafePlace.Models
{
    class Floor
    {
        public Guid Guid { get; set; }
        public IList<Camera> Cameras { get; set; }
        public BitmapImage FloorMap { set; get; }
        public string FloorName { set; get; }

        /// <summary>
        /// A method which allows to add a camera to the list while setting up the floor.
        /// </summary>
        /// <param name="camera">A dummy camera, put on a floor image in the floor planning window</param>
        public void AddCamera(Camera camera)
        {
            Cameras.Add(camera);
        }
    }
}
