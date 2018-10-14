using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace SafePlace.Models
{
    class Floor
    {
        //What about the setters and getters?
        List<Camera> cameras;
        Image FloorMap { set; get; }
        string FloorName { set; get; }

        //Creating a floor without any arguments should give it a placeholder image
        public Floor()
        {
            var img = new BitmapImage(new Uri("/Images/placeholder.png", UriKind.Relative));
            FloorMap = new Image
            {
                Source = img
            };
            cameras = new List<Camera>();
        }

        //Could try finding the image with given url, if it cannot be found, default constructor will be used.
        public Floor(string url, string name)
        {
            var img = new BitmapImage(new Uri(url, UriKind.Relative));
            FloorMap = new Image()
            {
                Source = img
            };
            FloorName = name;
        }

        /// <summary>
        /// A method which allows to add a camera to the list while setting up the floor.
        /// </summary>
        /// <param name="PlanningCamera">A dummy camera, put on a floor image in the floor planning window</param>
        public void AddCamera(Image PlanningCamera)
        {
            Camera camera = new Camera
            {
                Margin = PlanningCamera.Margin
            };
            cameras.Add(camera);
        }
    }
}
