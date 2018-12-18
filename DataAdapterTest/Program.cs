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
            var Floors = GetFloors();
            foreach (Floor floor in Floors)
            {
                Console.WriteLine(floor.Name + " " + floor.Guid + " " + floor.ImagePath);
            }
            Console.ReadLine();

        }
        #region SampleCode

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
            foreach(DataRow row in ds.Tables[0].Rows) Floors.Add(new Floor() {Name = (string)row["Name"], ImagePath = (string)row["ImagePath"], Guid = (Guid)row["Guid"] });
            return Floors;
        }

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

        static public void Update()
        {
            string myCon = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Vytautas\Desktop\aa.mdf;Integrated Security=True;Connect Timeout=30";

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = myCon;
            cn.Open();

            SqlCommand update = new SqlCommand();
            update.Connection = cn;
            update.CommandType = CommandType.Text;
            update.CommandText = "UPDATE Person SET FirstName = @FN, LastName = @LN WHERE Id = @Id";

            update.Parameters.Add(new SqlParameter("@FN", SqlDbType.NVarChar, 50, "FirstName"));
            update.Parameters.Add(new SqlParameter("@LN", SqlDbType.NVarChar, 50, "LastName"));
            update.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int, 50, "Id"));

            SqlDataAdapter da = new SqlDataAdapter("SELECT Id, FirstName, LastName FROM Person", cn);
            da.UpdateCommand = update;

            DataSet ds = new DataSet();
            da.Fill(ds, "Person");

            ds.Tables[0].Rows[0]["FirstName"] = "Jack";
            ds.Tables[0].Rows[0]["LastName"] = "Johnson";

            da.Update(ds.Tables[0]);
            cn.Close();
            da.Dispose();
        }
        #endregion
    }
}
