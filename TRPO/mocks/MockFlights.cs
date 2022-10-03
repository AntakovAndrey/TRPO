using Microsoft.Data.SqlClient;
using TRPO.interfaces;
using TRPO.Models;

namespace TRPO.mocks
{
    public class MockFlights:IAllFlights
    {
        DataBase dataBase = new DataBase();
        List<Flight> tmpFlights = new List<Flight>();
        public MockFlights() {}
        public IEnumerable<Flight> Flights
        {
            get {
                dataBase.openConnection();
                SqlCommand command = new SqlCommand("SELECT * FROM Flight", dataBase.GetConnection());
                SqlDataReader reader = command.ExecuteReader();
                
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        DateOnly date = DateOnly.FromDateTime(reader.GetDateTime(1));
                        TimeOnly startTime = TimeOnly.FromDateTime(reader.GetDateTime(2));
                        TimeOnly finishTime = TimeOnly.FromDateTime(reader.GetDateTime(3));
                        string startPoint = reader.GetString(4);
                        string finishPoint = reader.GetString(5);
                        int planeId = reader.GetInt32(6);
                        int crewId = reader.GetInt32(7);
                        tmpFlights.Add(new Flight(id, date, startTime, finishTime, startPoint, finishPoint, planeId, crewId));
                    }

                }
                dataBase.closeConnection();
                return Flights = new List<Flight>(tmpFlights); 
            }
            set
            {
                
            }
        } 
       

    }
}
