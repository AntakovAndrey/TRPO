
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Data.SqlClient;

namespace TRPO
{
    class DataBase
    {

        private static DataBase instance;

        private DataBase() { }

        public static DataBase getInstance()
        {
            if (instance == null)
                instance = new DataBase();
            return instance;
        }
        private static SqlConnection sqlConnection = new SqlConnection(@"Data Source = DESKTOP-30507DA;Initial catalog = trpo;Integrated Security = true;TrustServerCertificate = true");

        public void openConnection()
        {
            if(sqlConnection.State==System.Data.ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
        }
        public void closeConnection()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }
        public SqlConnection GetConnection()
        {
            return sqlConnection;
        }



    }
}
