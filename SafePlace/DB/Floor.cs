namespace SafePlace.DataBaseUtilioties
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Floor", Schema = "Topas")]
    public class Floor
    {

        public Floor()
        {
            this.Cameras = new HashSet<Camera>(); 
        }

        [Key]
        public Guid Guid { get; set; }

        [StringLength(255)]
        public string ImagePath { get; set; }

        [StringLength(64)]
        public string Name { get; set; }

        public virtual ICollection<Camera> Cameras { get; set; }
    }
}
