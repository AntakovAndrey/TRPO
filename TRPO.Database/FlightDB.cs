using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRPO.Services;
using TRPO.Models;
using Microsoft.Data.SqlClient;

namespace TRPO.Database
{
    internal class FlightDB
    {
        
        public static List<Flight> getDepartureToday()
        {
            SqlCommand command = new SqlCommand("SELECT * FROM Flight, [Route], Plane, Pilot" +
                " WHERE Flight.Route_id = [Route].Id AND Flight.Plane_id = Plane.Plane_id AND Flight.Pilot_id=Pilot.Pilot_id " +
                "AND Route.Start_point = @startPoint AND Flight.Date >= @dateToday ", DataBase.getInstance().getConnection());
            command.Parameters.Add("@startPoint", System.Data.SqlDbType.NChar, 20).Value = "Минск";
            command.Parameters.Add("@dateToday", System.Data.SqlDbType.Date).Value = DateTime.Now;
            return getFlightsByCommand(command);
        }
        public static List<Flight> getArrivlToady()
        {
            SqlCommand command = new SqlCommand("SELECT * FROM Flight, [Route], Plane, Pilot" +
                " WHERE Flight.Route_id = [Route].Id AND Flight.Plane_id = Plane.Plane_id AND Flight.Pilot_id=Pilot.Pilot_id " +
                 "AND Route.Finish_point = @finishPoint AND Flight.Date >= @dateToday ", DataBase.getInstance().getConnection());
            command.Parameters.Add("@finishPoint", System.Data.SqlDbType.NChar, 20).Value = "Минск";
            command.Parameters.Add("@dateToday", System.Data.SqlDbType.Date).Value = DateTime.Now;
            return getFlightsByCommand(command);
        }
        public void SaveUserToDB(Flight flight)
        {
            string commandExpression = "INSERT [Flight] (Date, StartTime, FinisshTime, FlightRoute, Plane, Pilot, Status)" +
                " VALUES (@Date, @StartTime, @FinisshTime, @FlightRoute, @Plane, @Pilot, @Status)";
            SqlCommand command = new SqlCommand(commandExpression, DataBase.getInstance().getConnection());
            command.Parameters.Add("@Name", System.Data.SqlDbType.Date).Value = flight.Date;
            command.Parameters.Add("@Surname", System.Data.SqlDbType.Time).Value = flight.StartTime;
            command.Parameters.Add("@PassportSeries", System.Data.SqlDbType.Time).Value = flight.FinishTime;
            command.Parameters.Add("@Nationality", System.Data.SqlDbType.NVarChar, 50).Value = flight.FlightRoute;
            command.Parameters.Add("@DateOfBirth", System.Data.SqlDbType.Int).Value = flight.Plane;
            command.Parameters.Add("@Telephone", System.Data.SqlDbType.Int).Value = flight.Pilot;
            command.Parameters.Add("@PassportNumber", System.Data.SqlDbType.Int).Value = flight.Status;
            DataBase.getInstance().openConnection();
            command.ExecuteNonQuery();
            DataBase.getInstance().closeConnection();
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
                    int flightId = reader.GetInt32(0);
                    DateOnly date = DateOnly.FromDateTime(reader.GetDateTime(1));
                    TimeOnly startTime = TimeOnly.FromTimeSpan(reader.GetTimeSpan(2));
                    TimeOnly finishTime = TimeOnly.FromTimeSpan(reader.GetTimeSpan(3));
                    string status;
                    try
                    {
                        status = reader.GetString(4);
                    }
                    catch (SqlNullValueException)
                    {
                        status = " ";
                    }
                    int pilotId = reader.GetInt32(5);
                    int planeId = reader.GetInt32(6);
                    int routeId = reader.GetInt32(7);
                    string startPoint = reader.GetString(9);
                    string finishPoint = reader.GetString(10);
                    int distance = reader.GetInt32(11);
                    string planeType = reader.GetString(13);
                    int maxFlightRange = reader.GetInt32(14);
                    int numberOfSeats = reader.GetInt32(15);
                    double fuelConsumption = reader.GetDouble(16);
                    string pilotName = reader.GetString(18);
                    string pilotSurname = reader.GetString(19);
                    tmpFlights.Add(new Flight(flightId, date, startTime, finishTime, status,
                        new Route(routeId, startPoint, finishPoint, distance),
                        new Plane(planeId, planeType, maxFlightRange, numberOfSeats, fuelConsumption),
                        new Pilot(pilotId, pilotName, pilotSurname))
                        );

                }
                DataBase.getInstance().closeConnection();
            }

            return tmpFlights;
        }
    }

}
    