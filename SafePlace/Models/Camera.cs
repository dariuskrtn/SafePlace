using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
/// <summary>
/// A class which represents any camera in a office building.
/// </summary>
namespace SafePlace.Models
{
    class Camera
    {
        public int floor;
        //Following 2 might be bad practice if we create a separate class which represents an icon of a camera in the UI
        public double X; 
        public double Y;

        //Need fields for connection and footage retrieval/display purposes
    }
}
