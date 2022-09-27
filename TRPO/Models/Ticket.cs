namespace TRPO.Models
{
    public class Ticket
    {
        public int TicketId { get; set; }
        public int PassangerId { get; set; }
        public int FlightId { get; set; }
        public double Price { get; set; }
        public string Class { get; set; }
        Ticket(int ticketId, int passangerId, int flightId, double price, string @class)
        {
            TicketId = ticketId;
            PassangerId = passangerId;
            FlightId = flightId;
            Price = price;
            Class = @class;
        }
    }
}
