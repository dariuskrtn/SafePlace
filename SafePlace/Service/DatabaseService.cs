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

        public void AddPerson(Guid guid, string name, string lastName, Guid personType = new Guid())
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
        }

    } // EndOfClass
}
    

