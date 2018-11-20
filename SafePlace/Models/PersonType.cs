namespace SafePlace.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    [Serializable]
    [Table("PersonType")]
    public partial class PersonType : Model
    {
        public PersonType()
        {
            this.AllowedCameras = new HashSet<Camera>();
        }

        [Key]
        public Guid Guid { get; set; }

        [StringLength(64)]
        public string Name { get; set; }

        public virtual ICollection<Camera> AllowedCameras { get; set; }
    }
}
