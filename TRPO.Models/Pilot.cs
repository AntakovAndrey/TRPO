using Microsoft.Data.SqlClient;
using TRPO.Services;

namespace TRPO.Models
{
    public class Pilot
    {
        public int PilotId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public Pilot(int pilotId, string name, string surname)
        {
            PilotId = pilotId;
            Name = name;
            Surname = surname;
        }
        public Pilot() { }

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
                    tmpFlights.Add(new Pilot(pilotId,name,surname));
                }
                DataBase.getInstance().closeConnection();
            }

            return tmpFlights;
        }
    }
}
