using SafePlace.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
/// <summary>
/// A class which represents any camera in a office building.
/// </summary>
namespace SafePlace.Models
{
    
    public partial class Camera
    {
        #region Fields

        #region DataBase part contains
        //public Guid Guid { get; set; }
        //public string IPAddress { get; set; }
        //public string Name { get; set; }        
        ////Position: X and Y mean the position of image's top left corner in relation to the top left corner of the floor image.
        ////Currently when put into the grid container of floor and camera images, the camera appears in the middle.
        //public int PositionX { get; set; }
        //public int PositionY { get; set; }
        #endregion

        //IdentifiedPeople will be used in the list, shown near a camera.
        [NotMapped]
        public IList<Person> IdentifiedPeople { get; set; }
        [NotMapped]
        public CameraStatus Status { get; set; }
        #endregion

    }
}
