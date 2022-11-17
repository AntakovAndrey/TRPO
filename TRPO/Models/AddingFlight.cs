namespace TRPO.Models
{
    public class AddingFlight
    {
        public DateTime Date { get; set; }
        public DateTime TimeUp { get; set; }
        public DateTime TimeIn { get; set; }
        public string DeparturePoint { get; set; }
        public string PlaceOfArrival { get; set; }
        public int FirstPilot { get; set; }
        public int SecondPilot { get; set; }
        public int NumberPlane { get; set; }

        AddingFlight(DateTime date, DateTime timeUp, DateTime timeIn, string departurePoint, string placeOfArrival, int firstPilot, int secondPilot, int numberPlane)
        {
            Date = date;
            TimeUp = timeUp;
            TimeIn = timeIn;
            DeparturePoint = departurePoint;
            PlaceOfArrival = placeOfArrival;
            FirstPilot = firstPilot;
            SecondPilot = secondPilot;
            NumberPlane = numberPlane;
        }
    }
}
