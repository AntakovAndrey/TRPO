using Microsoft.Data.SqlClient;

namespace TRPO.Database
{
    public class DataBase
    {
        private static DataBase _instance;

        private static SqlConnection _sqlConnection = new SqlConnection(@"Data Source = DESKTOP-30507DA;Initial catalog = trpo;Integrated Security = true;TrustServerCertificate = true");

        private DataBase() { }

        public static DataBase getInstance()
        {
            if(_instance == null)
                _instance = new DataBase();
            return _instance;
        }
        
        public void openConnection()
        {
            if(_sqlConnection.State==System.Data.ConnectionState.Closed)
            {
                _sqlConnection.Open();
            }
        }
        public void closeConnection()
        {
            if (_sqlConnection.State == System.Data.ConnectionState.Open)
            {
                _sqlConnection.Close();
            }
        }
        public SqlConnection GetConnection()
        {
            return _sqlConnection;
        }
    }
}