using MySql.Data.MySqlClient;
using System.Data;
namespace ProyectoProgra5.DataBaseHelper
{

    public class DataBaseWebHelper
    {
        const string database = "dbcondo";
        const string server = "localhost";
        MySqlConnection connection = new MySqlConnection($"server={server};database={database};uid=root;pwd=Admin$1234");

        public void TestConnection()
        {
            connection.Open();
            connection.Close();
        }

        public DataTable Fill(string storedProcedure, List<MySqlParameter>? param)
        {
            try
            {
                using (this.connection)
                {
                    connection.Open();
                    MySqlCommand cmd = connection.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = storedProcedure;

                    if (param != null)
                    {
                        foreach (MySqlParameter p in param)
                        {
                            cmd.Parameters.Add(p);
                        }
                    }

                    DataTable ds = new DataTable();
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(ds);
                    return ds;
                }
            }
            catch
            {
                throw;
            }
        }

        public void ExecuteQuery(string storedProcedure, List<MySqlParameter> param)
        {
            try
            {
                using (this.connection)
                {
                    connection.Open();
                    MySqlCommand cmd = connection.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = storedProcedure;

                    if (param != null)
                    {
                        foreach (MySqlParameter p in param)
                        {
                            cmd.Parameters.Add(p);
                        }
                    }

                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                throw;
            }
        }

        public DataTable SearchRecords(string searchValue)
        {
            try
            {
                List<MySqlParameter> parameters = new List<MySqlParameter>
    {
        new MySqlParameter("@searchValue", MySqlDbType.VarChar, 50) { Value = searchValue }
    };

                // Create a DataTable to hold the results
                DataTable result = new DataTable();

                // Execute the SearchRecords stored procedure for Person
                result = Fill("SearchRecords_Person", parameters);

                // Execute the SearchRecords stored procedure for House and merge the result
                result.Merge(Fill("SearchRecords_House", parameters));

                // Execute the SearchRecords stored procedure for Condos and merge the result
                result.Merge(Fill("SearchRecords_Condos", parameters));

                return result;
            }
            catch
            {
                throw;
            }
        }


    }

}
