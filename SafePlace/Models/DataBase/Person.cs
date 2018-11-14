namespace SafePlace.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Person", Schema = "Topas")]
    public partial class Person : Model
    {
        [Key]
        public Guid Guid { get; set; }

        [Required]
        [StringLength(64)]
        public string Name { get; set; }

        [Required]
        [StringLength(64)]
        public string LastName { get; set; }

        //Possible types: Employee (Various team roles + intern), Guest, BusinessCollaborator (visitors from foreign branches or related companies).
        //Could be in format: type, subtype. For example: "Visitor, business partner" or "Employee, intern".
        public PersonType PersonType { get; set; }

        public virtual ICollection<Camera> AllowedCameras { get; set; }
    }
}
