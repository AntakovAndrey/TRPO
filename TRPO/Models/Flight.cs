namespace TRPO.Models
{
    public class Flight
    {
        public int FlightId { get; set; }
        public DateTime Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly FinishTime { get; set; }
        public string StartPoint { get; set; }
        public string FinishPoint { get; set; }
        public int PlaneId { get; set; }
        public int CrewId { get; set; }

        public Flight(int flightId, DateTime date, TimeOnly startTime, TimeOnly finishTime, string startPoint, string finishPoint, int planeId, int crewId)
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
    }
   
}
