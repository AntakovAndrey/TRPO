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
        public int PalaneId { get; set; }
        public int CrewId { get; set; }
    }
}
