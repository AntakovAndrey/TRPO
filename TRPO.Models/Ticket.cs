namespace TRPO.Models
{
    public class Ticket
    {
        public int TicketId { get; }
        public int UserId { get; }
        public int FlightId { get; }
        public double Price { set; get; }
        public string Class { set; get; }
        public Ticket(int id, int ticketId, int flightId, double price, string ticketClass)
        {
            UserId = id;
            TicketId = ticketId;
            FlightId = flightId;
            Price = price;
            Class = ticketClass;   
        }
    }
}