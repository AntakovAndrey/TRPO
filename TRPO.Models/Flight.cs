using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Data.SqlTypes;
using TRPO.Interfaces;
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
        public string? Status { get; set; }

        public Flight(int flightId, DateOnly date, TimeOnly startTime, TimeOnly finishTime, string status, Route route, int planeId, int crewId)
        {
            FlightId = flightId;
            Date = date;
            StartTime = startTime;
            FinishTime = finishTime;
            PlaneId = planeId;
            CrewId = crewId;
            FlightRoute = route;
            Status=status;
        }

        public void saveFlightToDB()
        {
            string commandExpression = "INSERT Flight (Date, Start_time, Finish_time, Status, Plane_id, Crew_id, Route_id)" +
                    " VALUES (@flightDate, @startTime, @finishTime, @status, @planeId, @crewId, @routeId)";
            SqlCommand command = new SqlCommand(commandExpression, DataBase.getInstance().getConnection());
            command.Parameters.Add("@flightDate", System.Data.SqlDbType.Date).Value = Date;
            command.Parameters.Add("@startTime", System.Data.SqlDbType.Time, 20).Value = StartTime;
            command.Parameters.Add("@finishTime", System.Data.SqlDbType.Time, 50).Value = FinishTime;
            command.Parameters.Add("@status", System.Data.SqlDbType.NVarChar, 50).Value = Status;
            command.Parameters.Add("@planeId", System.Data.SqlDbType.Int, 20).Value = PlaneId;
            command.Parameters.Add("@crewId", System.Data.SqlDbType.Int).Value = CrewId;
            command.Parameters.Add("@routeId", System.Data.SqlDbType.Int, 20).Value = FlightRoute.Id;
            DataBase.getInstance().openConnection();
            command.ExecuteNonQuery();
            DataBase.getInstance().closeConnection();
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
            SqlCommand command = new SqlCommand("SELECT * FROM Flight, Route " +
                "WHERE Flight.Route_id = Route.Id AND Route.Start_point = @startPoint AND Flight.Date >= @dateNow ", DataBase.getInstance().getConnection());
            command.Parameters.Add("@startPoint", System.Data.SqlDbType.NChar, 20).Value = "Минск";
            command.Parameters.Add("@dateNow", System.Data.SqlDbType.Date).Value = DateTime.Now;
            return getFlightsByCommand(command);
        }
        public static List<Flight> getArrivlToady()
        {
            SqlCommand command = new SqlCommand("SELECT * FROM Flight, Route " +
                "WHERE Flight.Route_id = Route.Id AND Route.Finish_point = @finishPoint AND Flight.Date >= @dateNow ", DataBase.getInstance().getConnection());
            command.Parameters.Add("@finishPoint", System.Data.SqlDbType.NChar, 20).Value = "Минск";
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
                    TimeOnly startTime = TimeOnly.FromTimeSpan(reader.GetTimeSpan(2));
                    TimeOnly finishTime = TimeOnly.FromTimeSpan(reader.GetTimeSpan(3));
                    string status;
                    try
                    {
                        status = reader.GetString(4);
                    }
                    catch(SqlNullValueException)
                    {
                        status = " ";
                    }
                    int planeId = reader.GetInt32(5);
                    int crewId = reader.GetInt32(6);
                    int routeId = reader.GetInt32(7);
                    string startPoint = reader.GetString(9);
                    string finishPoint = reader.GetString(10);
                    int distance = reader.GetInt32(11);
                    tmpFlights.Add(new Flight(id, date, startTime, finishTime, status ,new Route(id,startPoint,finishPoint,distance), planeId, crewId));
                }
            }
            DataBase.getInstance().closeConnection();
            return tmpFlights;
        }

    }
   
}