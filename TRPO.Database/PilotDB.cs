﻿using TRPO.Models;
using Microsoft.Data.SqlClient;

namespace TRPO.Database
{
    public class PilotDB
    {
        public static List<Pilot> getAllPilots()
        {
            SqlCommand command = new SqlCommand("SELECT * FROM Pilot", DataBase.getInstance().getConnection());
            return getPilotsByCommand(command);
        }

        private static List<Pilot> getPilotsByCommand(SqlCommand command)
        {
            List<Pilot> tmpFlights = new List<Pilot>();
            DataBase.getInstance().openConnection();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int pilotId = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    string surname = reader.GetString(2);
                    tmpFlights.Add(new Pilot(pilotId, name, surname));
                }
                DataBase.getInstance().closeConnection();
            }
            return tmpFlights;
        }

        public static void SavePilotToDB(Pilot pilot)
        {
            string commandExpression = "INSERT [Pilot] (Name, Surname)" +
                " VALUES (@Name, @Surname)";
            SqlCommand command = new SqlCommand(commandExpression, DataBase.getInstance().getConnection());
            command.Parameters.Add("@Name", System.Data.SqlDbType.NChar, 20).Value = pilot.Name;
            command.Parameters.Add("@Surname", System.Data.SqlDbType.NChar, 20).Value = pilot.Surname;
            DataBase.getInstance().openConnection();
            command.ExecuteNonQuery();
            DataBase.getInstance().closeConnection();
        }
    }
}