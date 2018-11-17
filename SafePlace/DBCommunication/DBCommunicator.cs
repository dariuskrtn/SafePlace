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

    class DBCommunicator : IDBCommunicator
    {
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

        #region Get from FB
        public IDictionary<Guid, Camera> GetCameras()
        {
            using (DataContext dataContext = new DataContext())
            {
                return dataContext.Cameras.ToDictionary(e => e.Guid);
            }
        }

        public IDictionary<Guid, Floor> GetFloors()
        {
            using (DataContext dataContext = new DataContext())
            {
                return dataContext.Floors.ToDictionary(e => e.Guid);
            }
        }

        public IDictionary<Guid, Person> GetPeople()
        {
            using (DataContext dataContext = new DataContext())
            {
                return dataContext.People.ToDictionary(e => e.Guid);
            }
        }

        public IDictionary<Guid, PersonType> GetPersonTypes()
        {
            using (DataContext dataContext = new DataContext())
            {
                return dataContext.PersonTypes.ToDictionary(e => e.Guid);
            }
        }
        #endregion

        /// <summary>
        /// Update any model which extends Model.cs
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>

        public void Update<T> (T model) where T : Model
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
