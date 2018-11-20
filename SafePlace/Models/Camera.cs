namespace SafePlace.Models
{
    using SafePlace.Enums;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Serializable]
    [Table("Camera")]
    public partial class Camera : Model
    {
        #region database fiels
        public Camera()
        {
            this.People = new HashSet<Person>();
            this.PersonTypes = new HashSet<PersonType>();
        }

        [Key]
        public Guid Guid { get; set; }

        [StringLength(64)]
        public string IPAddress { get; set; }

        [StringLength(64)]
        public string Name { get; set; }

        public int PositionX { get; set; }

        public int PositionY { get; set; }

        public Floor Floor { get; set; }

        public ICollection<Person> People { set; get; }

        public ICollection<PersonType> PersonTypes { set; get; }
        #endregion

        #region extra fields
        //IdentifiedPeople will be used in the list, shown near a camera.
        [NotMapped]
        public IList<Person> IdentifiedPeople { get; set; }
        [NotMapped]
        public CameraStatus Status { get; set; }
        #endregion
    }
}
