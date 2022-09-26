namespace TRPO.Models
{
    public class Ticket
    {
        public int TicketId { get; set; }
        public int PassangerId { get; set; }
        public int FlightId { get; set; }
        public double Price { get; set; }
        public string Class { get; set; }
    }
}
