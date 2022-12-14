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
    public class FlightDB
    {
        public static List<Flight> getByID()
        {
            SqlCommand command = new SqlCommand("SELECT * FROM Flight, [Route], Plane, Pilot " +
                " WHERE  Flight.Route_id = [Route].Id AND Flight.Plane_id = Plane.Plane_id AND Flight.Pilot_id=Pilot.Pilot_id "
                , DataBase.getInstance().getConnection());
            return getFlightsByCommand(command);
        }

        public static List<Flight> getAllFlights()
        {
            SqlCommand command = new SqlCommand("SELECT * FROM Flight, [Route], Plane, Pilot" +
                " WHERE Flight.Route_id = [Route].Id AND Flight.Plane_id = Plane.Plane_id AND Flight.Pilot_id=Pilot.Pilot_id"
                , DataBase.getInstance().getConnection());
            
            return getFlightsByCommand(command);
        }
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

        public static void UpdateFlight(ModifyFlightViewModel modifiedFlight)
        {
            if(modifiedFlight.Date>=DateTime.Today)
            {
                SqlCommand command = new SqlCommand("UPDATE Flight SET Date = @date WHERE Flight_id = @id", DataBase.getInstance().getConnection());
                command.Parameters.Add("date", System.Data.SqlDbType.Date).Value = modifiedFlight.Date;
                command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = modifiedFlight.FlightId;
                DataBase.getInstance().openConnection();
                command.ExecuteNonQuery();
                DataBase.getInstance().closeConnection();
            }
            if(modifiedFlight.StartTime!=TimeSpan.Zero)
            {
                SqlCommand command = new SqlCommand("UPDATE Flight SET Start_time = @startTime WHERE Flight_id = @id", DataBase.getInstance().getConnection());
                command.Parameters.Add("@startTime", System.Data.SqlDbType.Time).Value = modifiedFlight.StartTime;
                command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = modifiedFlight.FlightId;
                DataBase.getInstance().openConnection();
                command.ExecuteNonQuery();
                DataBase.getInstance().closeConnection();
            }
            if(modifiedFlight.FinishTime!=TimeSpan.Zero)
            {
                SqlCommand command = new SqlCommand("UPDATE Flight SET Finish_time = @finishTime WHERE Flight_id = @id", DataBase.getInstance().getConnection());
                command.Parameters.Add("@finsihTime", System.Data.SqlDbType.Time).Value = modifiedFlight.FinishTime;
                command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = modifiedFlight.FlightId;
                DataBase.getInstance().openConnection();
                command.ExecuteNonQuery();
                DataBase.getInstance().closeConnection();
            }
            if(modifiedFlight.Status!=null)
            {
                SqlCommand command = new SqlCommand("UPDATE Flight SET Status = @status WHERE Flight_id = @id", DataBase.getInstance().getConnection());
                if(modifiedFlight.Status=="-")
                {
                    command.Parameters.Add("@status", System.Data.SqlDbType.NVarChar, 50).Value = " ";
                }
                else
                {
                    command.Parameters.Add("@status", System.Data.SqlDbType.NVarChar, 50).Value = modifiedFlight.Status;
                }
                command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = modifiedFlight.FlightId;
                DataBase.getInstance().openConnection();
                command.ExecuteNonQuery();
                DataBase.getInstance().closeConnection();
            }
        }

        public static void SaveFlightToDB(Flight flight)
        {
            string commandExpression = "INSERT Flight (Date, Start_time, Finish_time, Status,Pilot_id,Plane_id, Route_id)" +
                " VALUES (@Date, @StartTime, @FinishTime,@Status,@Pilot,@Plane,@FlightRoute)";
            SqlCommand command = new SqlCommand(commandExpression, DataBase.getInstance().getConnection());
            command.Parameters.Add("@Date", System.Data.SqlDbType.Date).Value = flight.Date;
            command.Parameters.Add("@StartTime", System.Data.SqlDbType.Time).Value = flight.StartTime;
            command.Parameters.Add("@FinishTime", System.Data.SqlDbType.Time).Value = flight.FinishTime;
            command.Parameters.Add("@FlightRoute", System.Data.SqlDbType.Int).Value = flight.RouteId;
            command.Parameters.Add("@Plane", System.Data.SqlDbType.Int).Value = flight.PlaneId;
            command.Parameters.Add("@Pilot", System.Data.SqlDbType.Int).Value = flight.PilotId;
            if(flight.Status!=null)
            {
                command.Parameters.Add("@Status", System.Data.SqlDbType.NVarChar, 50).Value = flight.Status;
            }
            else
            {
                command.Parameters.Add("@Status", System.Data.SqlDbType.NVarChar, 50).Value = " ";
            }
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
                    DateTime date = reader.GetDateTime(1);
                    TimeSpan startTime = reader.GetTimeSpan(2);
                    TimeSpan finishTime = reader.GetTimeSpan(3);
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