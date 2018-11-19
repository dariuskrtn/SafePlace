using SafePlace.DB;
using SafePlace.Models;
using SafePlace.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;

namespace SafePlace.DBCommunication
{

    public sealed class DBCommunicator
    {
        private static readonly Lazy<DBCommunicator> lazy = new Lazy<DBCommunicator>(() => new DBCommunicator());

        public static DBCommunicator Instace { get { return lazy.Value; } }


        #region Add to DB
        public void AddCamera(Camera camera)
        {
            // Would this give any use? I ques EF covers it?
            //if (camera == null)
            //    throw new NullReferenceException("Camera is not created.");

            using (DataContext dataContext = new DataContext())
            {
                dataContext.Cameras.Add(camera);
                dataContext.SaveChanges();
            }
        }

        public void AddFloor(Floor floor)
        {
            using (DataContext dataContext = new DataContext())
            {
                dataContext.Floors.Add(floor);
                dataContext.SaveChanges();
            }
        }

        public void AddPerson(Person person)
        {
            using (DataContext dataContext = new DataContext())
            {
                dataContext.People.Add(person);
                dataContext.SaveChanges();
            }
        }

        public void AddPersonType(PersonType personType)
        {
            using (DataContext dataContext = new DataContext())
            {
                dataContext.PersonTypes.Add(personType);
                dataContext.SaveChanges();
            }
        }
        #endregion

        #region Get from DB
        /// ToList() needed to invoke query, without if query is invoked when program tries to use
        /// returned data what is imposible, because dataContext at that momemt is already disposed

        public IEnumerable<Camera> GetCameras()
        {
            using (DataContext dataContext = new DataContext())
            {
                /// ToList deals with deferred excefution problem, (explained at the top af region)
                return dataContext.Cameras.AsEnumerable<Camera>().ToList(); ;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Floor> GetFloors()
        {
            using (DataContext dataContext = new DataContext())
            {
                /// Include gets floors together - Eager loading (Without it it would be lazy loading, what means
                /// related entities are not loaded)
                /// /// ToList deals with deferred excefution problem, (explained at the top af region)
                return dataContext.Floors.Include("cameras").AsEnumerable().ToList();
            }
        }

        public IEnumerable<Person> GetPeople()
        {
            using (DataContext dataContext = new DataContext())
            {
                /// ToList deals with deferred excefution problem, (explained at the top af region)
                return dataContext.People.AsEnumerable().ToList();
            }
        }

        public IEnumerable<PersonType> GetPersonTypes()
        {
            using (DataContext dataContext = new DataContext())
            {
                /// ToList deals with deferred excefution problem, (explained at the top af region)
                return dataContext.PersonTypes.AsEnumerable().ToList();
            }
        }

        public Person GetPerson(Guid Guid)
        {
            using (DataContext dataContext = new DataContext())
            {
                return dataContext.People.Find(Guid);
            }
        }

        public Floor GetFloor(Guid Guid)
        {
            using (DataContext dataContext = new DataContext())
            {
                return dataContext.Floors.Find(Guid);
            }
        }
        public Camera Getcamera(Guid Guid)
        {
            using (DataContext dataContext = new DataContext())
            {
                return dataContext.Cameras.Find(Guid);
            }
        }

        public PersonType GetPersonType(Guid Guid)
        {
            using (DataContext dataContext = new DataContext())
            {
                return dataContext.PersonTypes.Find(Guid);
            }
        }
        #endregion

        /// <summary>
        /// Update any model which extends Model.cs
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>

        public void Update<T>(T model) where T : Model
        {
            using (DataContext dataContext = new DataContext())
            {
                dataContext.Entry(model).CurrentValues.SetValues(model);
                // SaveChanges() actually updates the database
                dataContext.SaveChanges();
            }
        }
    }
}
