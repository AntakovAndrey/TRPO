using Microsoft.Data.SqlClient;
using TRPO.Models;
using TRPO.Services;

namespace TRPO.mocks
{
    public class MockPilots
    {
        List<Pilot> tmpPilots = new List<Pilot>();
        public IEnumerable<Pilot> Pilots
        {
            get
            {
                DataBase.getInstance().openConnection();
                SqlCommand command = new SqlCommand("SELECT * FROM Pilot", DataBase.getInstance().getConnection());
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
                DataBase.getInstance().closeConnection();
                return  tmpPilots;
            }
        }
    }
}