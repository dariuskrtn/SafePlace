namespace SafePlace.DataBaseUtilioties
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;


    [Table("Camera", Schema = "Topas")]
    public class Camera
    {

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

        public int? PositionX { get; set; }

        public int? PositionY { get; set; }

        public Floor Floor { get; set; }

        public virtual ICollection<Person> People { set; get; }

        public virtual ICollection<PersonType> PersonTypes { set; get; }
    }
}
