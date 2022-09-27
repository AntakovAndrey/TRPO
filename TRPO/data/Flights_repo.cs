using Microsoft.Data.SqlClient;
using TRPO.Models;

namespace TRPO.data
{
    public class Flights_repo
    {
        IEnumerable<Flight> flights { get; set; }
        public void Flight_repo()
        {
            SetAllFlight();
        }
        private void SetAllFlight()
        {
            DataBase dataBase = new DataBase();
            SqlCommand command = new SqlCommand("SELECT * FROM Flight WHERE Passanger_id = @uI ", dataBase.GetConnection());
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows) // если есть данные
            {
                // выводим названия столбцов
                while (reader.Read()) // построчно считываем данные
                {
                    int id = reader.GetInt32(0);
                    DateTime date = reader.GetDateTime(1);
                    TimeOnly startTime = TimeOnly.FromDateTime(reader.GetDateTime(2));
                    TimeOnly finishTime = TimeOnly.FromDateTime(reader.GetDateTime(3));
                    string startPoint = reader.GetString(4);
                    string finishPoint = reader.GetString(5);
                    int planeId = reader.GetInt32(6);
                    int crewId = reader.GetInt32(7);
                    flights.Append(new Flight(id, date, startTime, finishTime, startPoint, finishPoint, planeId, crewId));

                }

            }
        }
    }
}
