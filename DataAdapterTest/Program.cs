using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using SafePlace.Models;

namespace DataAdapterTest
{
    class Program
    {
        static string MyConnection = "Server=tcp:smartvision.database.windows.net,1433;Initial Catalog=SafePlaceDatabase;Persist Security Info=False;User ID=SmartVision2018;Password=TDVData2018;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        static void Main(string[] args)
        {
            //Getting floors from the database.
            var Floors = GetFloors();
            int i = 0;
            foreach (Floor floor in Floors)
            {
                Console.WriteLine(i++ + ". " +floor.Name + " " + floor.Guid + " " + floor.ImagePath);
            }

            Console.WriteLine("Which floor do you want to edit? (Write the index: 0, 1, ...)");
            i = Console.Read() - '0';
            //Read leaves end line symbol in the buffer, therefore we must remove it.
            Console.ReadLine();
            Console.WriteLine(i);
            if (i < 0 || i > 9)
            {
                Console.WriteLine("Wrong input. I give up.");
                return;
            }
            Console.WriteLine("Enter new name for the floor.");
            string newName = Console.ReadLine();
            //Calling update function with given information.
            if (!string.IsNullOrEmpty(newName) && i < Floors.Count) UpdateFloor(Floors.ToList()[i], newName);
            else Console.WriteLine("Wrong input. You must write a non empty name and the index of an existing floor.");
            
            Console.ReadLine();
        }
        //Gets all rows from the azure database floors table and converts them to SafePlace Floor objects.
        static public ICollection<Floor> GetFloors()
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = MyConnection;
            cn.Open();
            SqlDataAdapter da = new SqlDataAdapter("SELECT Guid, Name, ImagePath FROM Topas.Floor", cn);

            DataSet ds = new DataSet();
            da.Fill(ds, "Floor");

            cn.Close();
            da.Dispose();

            var Floors = new List<Floor>();
            foreach (DataRow row in ds.Tables[0].Rows) Floors.Add(new Floor() { Name = (string)row["Name"], ImagePath = (string)row["ImagePath"], Guid = (Guid)row["Guid"] });
            return Floors;
        }
        //Updates the name of a floor in the database given its representing object.
        static public void UpdateFloor(Floor floor, string newName)
        {

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = MyConnection;
            connection.Open();

            SqlCommand update = new SqlCommand();
            update.Connection = connection;
            update.CommandType = CommandType.Text;
            update.CommandText = "UPDATE Topas.Floor SET Name = @name WHERE Guid = @Id";

            update.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar, 64, "Name"));
            update.Parameters.Add(new SqlParameter("@Id", SqlDbType.UniqueIdentifier, 50, "Guid"));

            SqlDataAdapter adapter = new SqlDataAdapter("SELECT Guid, Name FROM Topas.Floor", connection);
            adapter.UpdateCommand = update;

            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, "Floor");

            var rows = dataSet.Tables[0].Rows;
            int rowToEdit = -1;
            foreach (DataRow row in rows)
            {
                if ((Guid)row["Guid"] == floor.Guid)
                {
                    Console.WriteLine("Row to edit found!");
                    rowToEdit = rows.IndexOf(row);
                    break;
                }

            }
            if (rowToEdit == -1) return;
            rows[rowToEdit]["Name"] = newName;
            //Not sure how the update function works. Perhaps the remote data set is compared with the local one and depending on the update query certain columns are changed.
            adapter.Update(dataSet.Tables[0]);
            connection.Close();
            adapter.Dispose();
        }

        #region SampleCode


        static public void SelectWithSelectCommand()
        {
            string myCon = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Vytautas\Desktop\aa.mdf;Integrated Security=True;Connect Timeout=30";

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = myCon;
            cn.Open();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand command = new SqlCommand("SELECT Id, FirstName, LastName FROM Person" +
                " WHERE FirstName = @FN and LastName = @LN", cn);

            command.Parameters.AddWithValue("@FN", "Jona1s");
            command.Parameters.AddWithValue("@LN", "Jonaitis");

            da.SelectCommand = command;

            DataSet ds = new DataSet();
            da.Fill(ds, "Person");

            cn.Close();
            da.Dispose();

            Console.WriteLine(ds.Tables[0].Rows[0]["LastName"]);
        }

        static public void Insert()
        {
            string myCon = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Vytautas\Desktop\aa.mdf;Integrated Security=True;Connect Timeout=30";

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = myCon;
            cn.Open();

            SqlCommand insert = new SqlCommand();
            insert.Connection = cn;
            insert.CommandType = CommandType.Text;
            insert.CommandText = "INSERT INTO Person (FirstName, LastName) VALUES (@FN,@LN)";

            insert.Parameters.Add(new SqlParameter("@FN", SqlDbType.NVarChar, 50, "FirstName"));
            insert.Parameters.Add(new SqlParameter("@LN", SqlDbType.NVarChar, 50, "LastName"));

            SqlDataAdapter da = new SqlDataAdapter("SELECT Id, FirstName, LastName FROM Person", cn);
            da.InsertCommand = insert;

            DataSet ds = new DataSet();
            da.Fill(ds, "Person");

            DataRow newRow = ds.Tables[0].NewRow();
            newRow["FirstName"] = "Jane";
            newRow["LastName"] = "Doe";
            ds.Tables[0].Rows.Add(newRow);

            da.Update(ds.Tables[0]);
            cn.Close();
            da.Dispose();
        }

        static public void Delete()
        {
            string myCon = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Vytautas\Desktop\aa.mdf;Integrated Security=True;Connect Timeout=30";

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = myCon;
            cn.Open();

            SqlCommand delete = new SqlCommand();
            delete.Connection = cn;
            delete.CommandType = CommandType.Text;
            delete.CommandText = "DELETE FROM Person WHERE Id = @Id";

            delete.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int, 50, "Id"));

            SqlDataAdapter da = new SqlDataAdapter("SELECT Id, FirstName, LastName FROM Person", cn);
            da.DeleteCommand = delete;

            DataSet ds = new DataSet();
            da.Fill(ds, "Person");

            ds.Tables[0].Rows[0].Delete();

            da.Update(ds.Tables[0]);
            cn.Close();
            da.Dispose();
        }

        
        #endregion
    }
}
