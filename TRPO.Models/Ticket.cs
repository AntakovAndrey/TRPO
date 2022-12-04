using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRPO.Models
{
    public class Ticket
    {
        public int Id { get; }
        public int TicketId { get; }
        public int FlightId { get; }
        public double Price { set; get; }
        public string Clas { set; get; }
        public Ticket(int id, int ticketId, int flightId, double price, string clas)
        {
            Id = id;
            TicketId = ticketId;
            FlightId = flightId;
            Price = price;
            Clas = clas;
          
        }
    }
}
