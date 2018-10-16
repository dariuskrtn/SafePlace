using System;
using System.Collections.Generic;
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
    class Camera
    {
        #region Fields
        public Guid Guid { get; set; }
        //IdentifiedPeople will be used in the list, shown near a camera.
        public List<Person> IdentifiedPeople { get; set; }
        public string IPAddress { get; set; }
        public string Name { get; set; }
        //Position: X and Y mean the position of image's top left corner in relation to the top left corner of the floor image.
        //Currently when put into the grid container of floor and camera images, the camera appears in the middle.
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public TransformGroup Transform { get; set; }
        #endregion
                   

        #region Methods
        /// <summary>
        /// This method adds a person to the list of recognised people. A person should be displayed near
        /// </summary>
        /// <param name="recognisedPerson"></param>
        public void AddPerson(Person recognisedPerson)
        {
            //Perhaps additional checking should be added to prevent various weird situations, such as 2 cameras constantly removing same people from each other
            if (recognisedPerson == null) return;
            if (recognisedPerson.Camera != null)
            {
                recognisedPerson.Camera.RemovePerson(recognisedPerson);
            }
            IdentifiedPeople.Add(recognisedPerson);
            recognisedPerson.Camera = this;
        }
        public void RemovePerson(Person person)
        {
            IdentifiedPeople.Remove(person);
        }
        #endregion

    }
}
