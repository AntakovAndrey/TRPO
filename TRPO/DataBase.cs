
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Data.SqlClient;

//using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
//using System.Windows.Forms;

namespace TRPO
{
    class DataBase
    {
        SqlConnection sqlConnection = new SqlConnection(@"Data Source = DESKTOP-30507DA;Initial catalog = trpo;Integrated Security = true;TrustServerCertificate = true");

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
