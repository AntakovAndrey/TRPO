﻿using Microsoft.Data.SqlClient;
using System.Data;
using TRPO.Models;

namespace TRPO.Database
{
    public class PlaneDB
    {
        
        public static Plane GetFromDBById(int id)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM Plane WHERE Plane_id = @id", DataBase.getInstance().getConnection());
            command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;
            return GetFromDBByCommand(command);
        }


        private static Plane GetFromDBByCommand(SqlCommand command)
        {
            DataRow[] planeInfo;
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(table);
            planeInfo = table.Select();
            if (planeInfo.Length > 0)
            {
                return new Plane(
                    Convert.ToInt32(planeInfo[0][0]),
                    Convert.ToString(planeInfo[0][1]),
                    Convert.ToInt32(planeInfo[0][2]),
                    Convert.ToInt32(planeInfo[0][3]),
                    Convert.ToDouble(planeInfo[0][4])
                );
            }
            else throw new Exception("Самолет с таким номером не найден.");
        }

        public static List<Plane> getAllPlanes()
        {
            SqlCommand command = new SqlCommand("SELECT * FROM Plane", DataBase.getInstance().getConnection());

            return getFlightsByCommand(command);
        }
        private static List<Plane> getFlightsByCommand(SqlCommand command)
        {
            List<Plane> tmpFlights = new List<Plane>();
            DataBase.getInstance().openConnection();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int planeId = reader.GetInt32(0);
                    string planeType = reader.GetString(1);
                    int maxFlightRange = reader.GetInt32(2);
                    int numberOfSeats = reader.GetInt32(3);
                    double fuelConsumption = reader.GetDouble(4);
                    tmpFlights.Add(new Plane(planeId, planeType, maxFlightRange, numberOfSeats, fuelConsumption));
                }
                DataBase.getInstance().closeConnection();
            }

            return tmpFlights;
        }
        public static void SavePlaneToDB(Plane plane)
        {
            string commandExpression = "INSERT [Plane] (Type, Max_flight_range, Number_of_seats, Fuel_consumtion)" +
                " VALUES (@Type, @MaxFlightRange, @NumberOfSeats, @FuelConsumtion)";
            SqlCommand command = new SqlCommand(commandExpression, DataBase.getInstance().getConnection());
            command.Parameters.Add("@Type", System.Data.SqlDbType.NChar, 20).Value = plane.Type;
            command.Parameters.Add("@MaxFlightRange", System.Data.SqlDbType.Int).Value = plane.MaxFlightRange;
            command.Parameters.Add("@NumberOfSeats", System.Data.SqlDbType.Int).Value = plane.NumberOfSeats;
            command.Parameters.Add("@FuelConsumtion", System.Data.SqlDbType.Float).Value = plane.FuelConsumtion;
            DataBase.getInstance().openConnection();
            command.ExecuteNonQuery();
            DataBase.getInstance().closeConnection();
        }
       
    }
}