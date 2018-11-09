using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafePlace.Service
{
    class DatabaseService : IDatabaseService
    {
        /// <summary>
        /// Makes connection with database
        /// </summary>
        private void ConnectToDB(string queryString)
        {
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "smartvision.database.windows.net";
                builder.UserID = "SmartVision2018";
                builder.Password = "TDVData2018";
                builder.InitialCatalog = "SafePlaceDatabase";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();


                    using (var command = new SqlCommand(queryString, connection))
                    {
                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine(rowsAffected + " = rows affected.");
                    }

                }
            }
            catch (SqlException e)
            {
                //TODO
                //_logger.log(e.ToString());
                Console.WriteLine(e.ToString());
            }
        }

        #region Adding to main tables
        public void AddPerson(Guid guid, string name, string lastName, ICollection<Guid> allowedCameras, Guid personType = new Guid())
        {
            // Person columns: (Guid, Name, LastName, PersonType)
            var queryString =
                @"INSERT INTO dbo.Person
                    (Guid, Name, LastName " + (personType != new Guid() ? ", personType" : "")  + @")
                  VALUES
               ('" + guid + "', '" + name + "', '" + lastName;

            if (personType != new Guid())
                queryString += "', '" + personType;

            queryString += "')";

            ConnectToDB(queryString);

            if (allowedCameras != null && allowedCameras.Count > 0)
                AddAllowedPersonCameras(guid, allowedCameras);
        }

        public void AddPerson(Guid guid, string name, string lastName, Guid personType = new Guid())
        {
            // Person columns: (Guid, Name, LastName, PersonType)
            var queryString =
                @"INSERT INTO dbo.Person
                    (Guid, Name, LastName " + (personType != new Guid() ? ", personType" : "") + @")
                  VALUES
               ('" + guid + "', '" + name + "', '" + lastName;

            if (personType != new Guid())
                queryString += "', '" + personType;

            queryString += "')";

            ConnectToDB(queryString);
        }

        public void AddCamera(Guid guid, string iPAdress, string name, int posX, int posY, Guid floor)
        {
            // Person columns: (Guid, Name, LastName, PersonType)
            var queryString =
                @"INSERT INTO dbo.Camera
                    (Guid, IPAddress, Name, PositionX, PositionY, Floor) -- Unnecessary since we always add to all columns
                  VALUES
               ('" + guid + "', '" + iPAdress + "', '" + name + "', '" + posX + "', '" + posY + "', '" + floor + "')";

            ConnectToDB(queryString);
        }

        public void AddFloor(Guid guid, string imagePath, string name)
        {
            // Person columns: (Guid, Name, LastName, PersonType)
            var queryString =
                @"INSERT INTO dbo.Floor
                    (Guid, ImagePath, Name) -- Unnecessary since we always add to all columns
                  VALUES
               ('" + guid + "', '" + imagePath + "', '" + name + "')";

            ConnectToDB(queryString);
        }

        public void AddPersonType(Guid guid, string name, ICollection<Guid> allowedCameras = null)
        {
            // Person columns: (Guid, Name, LastName, PersonType)
            var queryString =
                @"INSERT INTO dbo.PersonType
                    (Guid, Name) -- Unnecessary since we always add to all columns
                  VALUES
               ('" + guid + "', '" + name + "')";

            ConnectToDB(queryString);
            if (allowedCameras != null && allowedCameras.Count > 0)
                AddPersonTypeCameras(guid, allowedCameras);
        }
        #endregion
        #region Adding to helper tables
        /// <summary>
        /// Use only with intention to update person type!
        /// AddPersonType function calls this automatically if cameras are given
        /// Adds person type and its cameras to PersonTypeCameras table
        /// </summary>
        /// <param name="personTypeGuid"></param>
        /// <param name="cameras"></param>
        public void AddPersonTypeCameras(Guid personTypeGuid, ICollection<Guid> allowedCameras)
        {
            var queryString = "";
            foreach (var camera in allowedCameras)
                queryString += @"INSERT INTO dbo.PersonTypeCameras
                                (PersonType, camera)
                                VALUES
                                ( '" + personTypeGuid + "', '" + camera + "')" ;
 
            ConnectToDB(queryString);
        }

        /// <summary>
        /// Use only with intention to update person!
        /// AddPerson function calls this automatically if cameras are given
        /// Adds person and its alowed cameras to AllowedPersonCameras table
        /// </summary>
        /// <param name="personGuid"></param>
        /// <param name="allowedCameras"></param>
        public void AddAllowedPersonCameras(Guid personGuid, ICollection<Guid> allowedCameras)
        {
            var queryString = "";
            foreach (var camera in allowedCameras)
                queryString += @"INSERT INTO dbo.AllowedPersonCameras
                                (Person, camera)
                                VALUES
                                ( '" + personGuid + "', '" + camera + "')";

            ConnectToDB(queryString);
        }

        #endregion

    } // EndOfClass
}
    

