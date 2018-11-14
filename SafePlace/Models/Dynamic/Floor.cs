using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SafePlace.Models
{
    
    public partial class Floor
    {

        #region DB part contains
        //public string Name { set; get; }
        //public Guid Guid { get; set; }
        //public IList<Camera> Cameras { get; set; }
        #endregion

        [NotMapped]
        public BitmapImage FloorMap { set; get; }
        

        /// <summary>
        /// A method which allows to add a camera to the list while setting up the floor.
        /// </summary>
        /// <param name="camera">A dummy camera, put on a floor image in the floor planning window</param>
        public void AddCamera(Camera camera)
        {
            Cameras.Add(camera);
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
