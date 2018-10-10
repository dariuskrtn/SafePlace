using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
/// <summary>
/// A class which represents any camera in a office building.
/// </summary>
namespace SafePlace.Models
{
    class Camera : System.Windows.Controls.Image
    {
        #region Fields
        //url for the image
        private const string url = "/Images/camera.png";
        
        //IdentifiedPeople will be used in the list, shown near a camera.
        public List<Person> IdentifiedPeople { get; set; }
        
        // It would be simpler to have 2 icons near a camera: a green one and a red one. Both could have a number of spotted people.
        // They would appear depending on below 2 values.
        public bool SpottedTrespasser;
        public bool SpottedLegitPerson;

        #endregion
        //Need fields for camera footage and connection handling
                   
        #region Constructors
        public Camera()
        {
            this.Source = new BitmapImage(new Uri(url, UriKind.Relative));
            IdentifiedPeople = new List<Person>();
        }

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
