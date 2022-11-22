using Microsoft.Data.SqlClient;
using System;
using System.Data;
using TRPO.Services;

namespace TRPO.Models
{
    public class Flight
    {
        public int FlightId { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly FinishTime { get; set; }
        public Route FlightRoute { get; set; }
        public int PlaneId { get; set; }
        public int CrewId { get; set; }

        public Flight(int flightId, DateOnly date, TimeOnly startTime, TimeOnly finishTime, Route route, int planeId, int crewId)
        {
            FlightId = flightId;
            Date = date;
            StartTime = startTime;
            FinishTime = finishTime;
            PlaneId = planeId;
            CrewId = crewId;
            FlightRoute = route;
        }

        public void saveFlightsToDB()
        {   
        
        }

        //public static Flight GetByID(int id)
        //{
        //    DataRow[] flightInfo;
        //    SqlCommand command = new SqlCommand("SELECT * FROM Flight WHERE Flight_id = @id", DataBase.getInstance().getConnection());
        //    command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;
        //    DataTable table = new DataTable();
        //    SqlDataAdapter adapter = new SqlDataAdapter();
        //    adapter.SelectCommand = command;
        //    adapter.Fill(table);
        //    flightInfo = table.Select();
        //    return new Flight(
        //        Convert.ToInt32(flightInfo[0][0]),
        //        DateOnly.FromDateTime(Convert.ToDateTime(flightInfo[0][1])),
        //        TimeOnly.FromDateTime(Convert.ToDateTime(flightInfo[0][2])), 
        //        TimeOnly.FromDateTime(Convert.ToDateTime(flightInfo[0][3])), 
        //        Convert.ToString(flightInfo[0][4]), 
        //        Convert.ToString(flightInfo[0][5]), 
        //        Convert.ToInt32(flightInfo[0][6]), 
        //        Convert.ToInt32(flightInfo[0][7])
        //    );
        //}

        public static List<Flight> getDepartureToday()
        {
            SqlCommand command = new SqlCommand("SELECT * FROM Flight WHERE Start_point = @startPoint AND Date >= @dateNow ", DataBase.getInstance().getConnection());
            command.Parameters.Add("@startPoint", System.Data.SqlDbType.NChar, 20).Value = "Минск";
            command.Parameters.Add("@dateNow", System.Data.SqlDbType.Date).Value=DateTime.Now;
            return getFlightsByCommand(command);
        }
        public static List<Flight> getArrivlToady()
        {
            SqlCommand command = new SqlCommand("SELECT * FROM Flight, Route " +
                "WHERE Flight.Route_id = Route.Id AND Route.Finish_point = @startPoint AND Flight.Date >= @dateNow ", DataBase.getInstance().getConnection());
            command.Parameters.Add("@startPoint", System.Data.SqlDbType.NChar, 20).Value = "Минск";
            command.Parameters.Add("@dateNow", System.Data.SqlDbType.Date).Value = DateTime.Now;
            return getFlightsByCommand(command);
        }


        private static List<Flight> getFlightsByCommand(SqlCommand command)
        {
            List<Flight> tmpFlights = new List<Flight>();
            DataBase.getInstance().openConnection();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    DateOnly date = DateOnly.FromDateTime(reader.GetDateTime(1));
                    TimeOnly startTime = TimeOnly.FromDateTime(reader.GetDateTime(2));
                    TimeOnly finishTime = TimeOnly.FromDateTime(reader.GetDateTime(3));
                    int planeId = reader.GetInt32(4);
                    int crewId = reader.GetInt32(5);
                    int routeId = Convert.ToInt32(6);
                    string startPoint = reader.GetString(8);
                    string finishPoint = reader.GetString(9);
                    int distance = reader.GetInt32(10);
                    tmpFlights.Add(new Flight(id, date, startTime, finishTime, new Route(id,startPoint,finishPoint,distance), planeId, crewId));
                }
            }
            DataBase.getInstance().closeConnection();
            return new List<Flight>(tmpFlights);
        }

    }
   
}
