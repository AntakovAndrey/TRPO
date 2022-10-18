using Microsoft.Data.SqlClient;
using TRPO.interfaces;
using TRPO.Models;

namespace TRPO.mocks
{
    public class MockFlights:IAllFlights
    {
        SqlCommand command;
        public MockFlights(SqlCommand command)
        {
            this.command = command;
        }
        public MockFlights()
        {
            command = new SqlCommand("SELECT * FROM Flight", DataBase.getInstance().GetConnection());
        }
        List<Flight> tmpFlights = new List<Flight>();
        public IEnumerable<Flight> Flights
        {
            get {
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
                        string startPoint = reader.GetString(4);
                        string finishPoint = reader.GetString(5);
                        int planeId = reader.GetInt32(6);
                        int crewId = reader.GetInt32(7);
                        tmpFlights.Add(new Flight(id, date, startTime, finishTime, startPoint, finishPoint, planeId, crewId));
                    }
                }
                DataBase.getInstance().closeConnection();
                return Flights = new List<Flight>(tmpFlights); 
            }
            
            set
            {
                
            }
        }
    }
}
