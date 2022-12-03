using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRPO.Services;

namespace TRPO.Database
{
    internal class RouteDB
    {
        public int Id { get; set; }
        public string StartPoint { get; set; }
        public string FinishPoint { get; set; }
        public int Distance { get; set; }
        public static List<Route> getAllRoutes()
        {
            SqlCommand command = new SqlCommand("SELECT * FROM Route", DataBase.getInstance().getConnection());
            return getFlightsByCommand(command);
        }



        private static List<RouteDB> getFlightsByCommand(SqlCommand command)
        {
            List<RouteDB> tmpRoutes = new List<RouteDB>();
            DataBase.getInstance().openConnection();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string startPoint = reader.GetString(1);
                    string finishPoint = reader.GetString(2);
                    int distance = reader.GetInt32(3);

                    tmpRoutes.Add(new RouteDB(id, startPoint, finishPoint, distance));
                }
            }
            DataBase.getInstance().closeConnection();
            return tmpRoutes;
        }
        public void SaveUserToDB(RouteDB route)
        {
            string commandExpression = "INSERT [Route] (StartPoint, FinishPoint, Distance)" +
                " VALUES (@StartPoint, @FinishPoint, @Distance)"; 
            SqlCommand command = new SqlCommand(commandExpression, DataBase.getInstance().getConnection());
            command.Parameters.Add("@StartPoint", System.Data.SqlDbType.NVarChar, 50).Value = route.StartPoint;
            command.Parameters.Add("@FinishPoint", System.Data.SqlDbType.NVarChar, 50).Value = route.FinishPoint;
            command.Parameters.Add("@Distance", System.Data.SqlDbType.Int).Value = route.Distance;
            command.ExecuteNonQuery();
            DataBase.getInstance().closeConnection();
        }
    }
}
