using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRPO.Services;

namespace TRPO.Database
{
    internal class TicketDB
    {
        public int TicketId { get; set; }
        public string PassangerId { get; set; }
        public string FlightId { get; set; }
        public int Price { get; set; }
        public string Class { get; set; }
       
        public void SaveUserToDB(TicketDB ticket)
        {
            string commandExpression = "INSERT [Ticket] (Price, Class)" +
                " VALUES (@Price, @Class";
            SqlCommand command = new SqlCommand(commandExpression, DataBase.getInstance().getConnection());
            command.Parameters.Add("@Price", System.Data.SqlDbType.Float).Value = ticket.Price;
            command.Parameters.Add("@Class", System.Data.SqlDbType.NChar, 10).Value = ticket.Class;
            command.ExecuteNonQuery();
            DataBase.getInstance().closeConnection();
        }
    }
}
