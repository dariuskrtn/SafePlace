namespace SafePlace.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Windows.Media.Imaging;
    [Serializable]
    [Table("Floor")]
    public partial class Floor : Model
    {

        #region database fields

        [Key]
        public Guid Guid { get; set; }

        [StringLength(255)]
        public string ImagePath { get; set; }

        [StringLength(64)]
        public string Name { get; set; }


        #endregion

        #region extra stuff
        [NotMapped]
        public virtual ICollection<Camera> Cameras { get; set; }

        public Floor()
        {
            this.Cameras = new HashSet<Camera>(); 
        }

        
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

        #endregion
    }
}
