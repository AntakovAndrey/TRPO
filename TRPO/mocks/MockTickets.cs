using Microsoft.Data.SqlClient;
using TRPO.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TRPO.mocks
{
    public class MockTickets
    {
        SqlCommand _command;
        List<Ticket> ticket=new List<Ticket>();
        public IEnumerable<Ticket> Tickets
        {
            get
            {
                DataBase.getInstance().openConnection();
                SqlDataReader reader = _command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        int ticketId = reader.GetInt32(0);
                        int passangerId= reader.GetInt32(1);
                        int flightId= reader.GetInt32(2);
                        double price=reader.GetDouble(3);
                        string _class=reader.GetString(4);
                        ticket.Add(new Ticket(ticketId, passangerId, flightId, price, _class));
                    }
                }
                DataBase.getInstance().closeConnection();
                return Tickets = new List<Ticket>(ticket);
            }

            set
            {

            }
        }
    }
}
