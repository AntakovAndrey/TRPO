using TRPO.Models;
using Microsoft.Data.SqlClient;

namespace TRPO.Database
{
    public class RouteDB
    {
       
        public static List<Route> getAllRoutes()
        {
            SqlCommand command = new SqlCommand("SELECT * FROM Route", DataBase.getInstance().getConnection());
            return getRoutesByCommand(command);
        }

        public static void SaveRouteToDB(Route route)
        {
            string commandExpression = "INSERT [Route] (Start_point, Finish_point, Distance)" +
                " VALUES (@StartPoint, @FinishPoint, @Distance)"; 
            SqlCommand command = new SqlCommand(commandExpression, DataBase.getInstance().getConnection());
            command.Parameters.Add("@StartPoint", System.Data.SqlDbType.NVarChar, 50).Value = route.StartPoint;
            command.Parameters.Add("@FinishPoint", System.Data.SqlDbType.NVarChar, 50).Value = route.FinishPoint;
            command.Parameters.Add("@Distance", System.Data.SqlDbType.Int).Value = route.Distance;
            DataBase.getInstance().openConnection();
            command.ExecuteNonQuery();
            DataBase.getInstance().closeConnection();
        }
        
        private static List<Route> getRoutesByCommand(SqlCommand command)
        {
            List<Route> tmpRoutes = new List<Route>();
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

                    tmpRoutes.Add(new Route(id, startPoint, finishPoint, distance));
                }
            }
            DataBase.getInstance().closeConnection();
            return tmpRoutes;
        }
    }
}