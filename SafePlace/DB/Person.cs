namespace SafePlace.DataBaseUtilioties
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Person", Schema = "Topas")]
    public class Person
    {
        [Key]
        public Guid Guid { get; set; }

        [Required]
        [StringLength(64)]
        public string Name { get; set; }

        [Required]
        [StringLength(64)]
        public string LastName { get; set; }

        public Guid? PersonType { get; set; }

        public virtual ICollection<Camera> AllowedCameras { get; set; }
    }
}
