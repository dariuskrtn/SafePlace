namespace SafePlace.DB
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using SafePlace.Models;
    using System.Configuration;

    public class DataContext : DbContext
    {
        public DataContext()
            : base("name=DataContext")
        {
        }

        public virtual DbSet<Camera> Cameras { get; set; }
        public DbSet<Floor> Floors { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<PersonType> PersonTypes { get; set; }


        /// <summary>
        /// Configure entities
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Set default schema
            modelBuilder.HasDefaultSchema(ConfigurationManager.AppSettings["default-schema"]);

            //Configure a Many-to-Many Relationship using Fluent API
            //Not nesessary, I didn;t do this for second Many-to-Many, EF can configre automatcally
            modelBuilder.Entity<Person>()
                .HasMany<Camera>(a => a.AllowedCameras)
                .WithMany(p => p.People)
                .Map(ap =>
                {
                    ap.MapLeftKey("Person");
                    ap.MapRightKey("Camera");
                });

            #region Auto-generated
            modelBuilder.Entity<Camera>()
                .Property(e => e.IPAddress)
                .IsUnicode(false);

            modelBuilder.Entity<Camera>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Floor>()
                .Property(e => e.ImagePath)
                .IsUnicode(false);

            modelBuilder.Entity<Floor>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Person>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Person>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<PersonType>()
                .Property(e => e.Name)
                .IsUnicode(false);
            #endregion
        }
    }
}

