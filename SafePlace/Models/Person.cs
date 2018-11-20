namespace SafePlace.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    [Serializable]
    [Table("Person")]
    public partial class Person : Model
    {
        #region database fields
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
        #endregion

        #region extr stuff
        //Whenever a camera notices a person, following should happen:
        //1. If camera was not null, the person is removed from the camera's SpottedPeople list;
        //2. A new value is set and the person is added there.
        [NotMapped]
        public Camera Camera { set; get; }
        public Person() { }
        public Person(string name, string lastname)
        {
            this.Name = name;
            this.LastName = lastname;
        }

        override public string ToString()
        {
            return $"{Name} {LastName}";
        }
        #endregion
    }
}
