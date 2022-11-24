using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRPO.Services;

namespace TRPO.Models
{
    public class Route
    {
        public int Id { get; set; }
        public string StartPoint { get; set; }
        public string FinishPoint { get; set; }
        public int Distance { get; set; }

        public Route(int id, string startPoint, string finishPoint, int distance)
        {
            Id = id;
            StartPoint = startPoint;
            FinishPoint = finishPoint;
            Distance = distance;
        }
        public Route() { }

        public void saveToDB()
        {

        }

         

        public static List<Route> getAllRoutes()
        {
            SqlCommand command = new SqlCommand("SELECT * FROM Route", DataBase.getInstance().getConnection());
            return getFlightsByCommand(command);
        }



        private static List<Route> getFlightsByCommand(SqlCommand command)
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
