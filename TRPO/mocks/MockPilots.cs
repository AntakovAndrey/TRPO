using Microsoft.Data.SqlClient;
using TRPO.interfaces;
using TRPO.Models;

namespace TRPO.mocks
{
    public class MockPilots : IAllPilots
    {
        DataBase dataBase = new DataBase();
        List<Pilot> tmpPilots = new List<Pilot>();
        public IEnumerable<Pilot> Pilots
        {
            get
            {
                dataBase.openConnection();
                SqlCommand command = new SqlCommand("SELECT * FROM Pilot", dataBase.GetConnection());
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        string surname = reader.GetString(2);
                        string shedule = reader.GetString(3);
                        TimeOnly workHoursStart = TimeOnly.FromDateTime(reader.GetDateTime(4));
                        TimeOnly workHoursFinsh = TimeOnly.FromDateTime(reader.GetDateTime(5));
                        tmpPilots.Add(new Pilot(id, name, surname, shedule, workHoursStart, workHoursFinsh));
                    }
                }
                dataBase.closeConnection();
                return  tmpPilots;
            }
        }
    }
}
