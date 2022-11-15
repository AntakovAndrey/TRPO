using Microsoft.Data.SqlClient;
using System.Data;
using TRPO.Database;
using TRPO.interfaces;

namespace TRPO
{
    public class Flight
    {
        public int FlightId { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly FinishTime { get; set; }
        public string StartPoint { get; set; }
        public string FinishPoint { get; set; }
        public int PlaneId { get; set; }
        public int CrewId { get; set; }

        public Flight(int flightId, DateOnly date, TimeOnly startTime, TimeOnly finishTime, string startPoint, string finishPoint, int planeId, int crewId)
        {
            FlightId = flightId;
            Date = date;
            StartTime = startTime;
            FinishTime = finishTime;
            StartPoint = startPoint;
            FinishPoint = finishPoint;
            PlaneId = planeId;
            CrewId = crewId;
        }

        public static Flight GetByID(int id)
        {
            DataRow[] flightInfo;
            SqlCommand command = new SqlCommand("SELECT * FROM Flight WHERE Flight_id = @id", DataBase.getInstance().getConnection());
            command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(table);
            flightInfo = table.Select();
            return new Flight(
                Convert.ToInt32(flightInfo[0][0]),
                DateOnly.FromDateTime(Convert.ToDateTime(flightInfo[0][1])),
                TimeOnly.FromDateTime(Convert.ToDateTime(flightInfo[0][2])), 
                TimeOnly.FromDateTime(Convert.ToDateTime(flightInfo[0][3])), 
                Convert.ToString(flightInfo[0][4]), 
                Convert.ToString(flightInfo[0][5]), 
                Convert.ToInt32(flightInfo[0][6]), 
                Convert.ToInt32(flightInfo[0][7])
            );
        }
        //public IEnumerable<Flight> Flights
        //{
        //    get
        //    {
        //        DataBase.getInstance().openConnection();
        //        //SqlDataReader reader = command.ExecuteReader();
        //        if (reader.HasRows)
        //        {
        //            while (reader.Read())
        //            {

        //                int id = reader.GetInt32(0);
        //                DateOnly date = DateOnly.FromDateTime(reader.GetDateTime(1));
        //                TimeOnly startTime = TimeOnly.FromDateTime(reader.GetDateTime(2));
        //                TimeOnly finishTime = TimeOnly.FromDateTime(reader.GetDateTime(3));
        //                string startPoint = reader.GetString(4);
        //                string finishPoint = reader.GetString(5);
        //                int planeId = reader.GetInt32(6);
        //                int crewId = reader.GetInt32(7);
        //                tmpFlights.Add(new Flight(id, date, startTime, finishTime, startPoint, finishPoint, planeId, crewId));
        //            }
        //        }
        //        DataBase.getInstance().closeConnection();
        //        return Flights = new List<Flight>(tmpFlights);
        //    }

        //    set
        //    {

        //    }
        //}



    }
   
}
