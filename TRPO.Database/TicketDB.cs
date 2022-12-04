using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRPO.Services;
using TRPO.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace TRPO.Database
{
    internal class TicketDB
    {
        public static Ticket GetFromDBById(int id)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM [Ticket] WHERE Ticket_id = @id", DataBase.getInstance().getConnection());
            command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;
            return GetFromDBByCommand(command);
        }
        public static Ticket GetFromDBByUserId(int UserId)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM [Ticket] WHERE Passanger_id = @id", DataBase.getInstance().getConnection());
            command.Parameters.Add("@UserId", System.Data.SqlDbType.Int).Value = UserId;
            return GetFromDBByCommand(command);
        }
        public static Ticket GetFromDBByFlightID(int FlightId)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM [Ticket] WHERE Flight_id = @id", DataBase.getInstance().getConnection());
            command.Parameters.Add("@FlightId", System.Data.SqlDbType.Int).Value = FlightId;
            return GetFromDBByCommand(command);
        }
        public static Ticket GetFromDBByPrice(float price)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM [Ticket] WHERE Price= @id", DataBase.getInstance().getConnection());
            command.Parameters.Add("@price", System.Data.SqlDbType.Float).Value = price;
            return GetFromDBByCommand(command);
        }
        public static Ticket GetFromDBByClass(string clas)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM [Ticket] WHERE Class = @id", DataBase.getInstance().getConnection());
            command.Parameters.Add("@class", System.Data.SqlDbType.NChar).Value = clas;
            return GetFromDBByCommand(command);
        }
    private static Ticket GetFromDBByCommand(SqlCommand command)
    {
        DataRow[] ticketInfo;
        DataTable table = new DataTable();
        SqlDataAdapter adapter = new SqlDataAdapter();
        adapter.SelectCommand = command;
        adapter.Fill(table);
        ticketInfo = table.Select();
        if (ticketInfo.Length > 0)
        {
            return new Ticket(
                id: Convert.ToInt32(ticketInfo[0][0]),
                ticketId: Convert.ToInt32(ticketInfo[0][1]),
                flightId: Convert.ToInt32(ticketInfo[0][2]),
                price: Convert.ToDouble(ticketInfo[0][3]),
                clas: Convert.ToString(ticketInfo[0][4])
            );
        }
        else return null;
    }
    public void SaveUserToDB(Ticket ticket)
        {
            string commandExpression = "INSERT [Ticket] (Price, Class)" +
                " VALUES (@Price, @Class";
            SqlCommand command = new SqlCommand(commandExpression, DataBase.getInstance().getConnection());
            command.Parameters.Add("@Price", System.Data.SqlDbType.Float).Value = ticket.Price;
            command.Parameters.Add("@Class", System.Data.SqlDbType.NChar, 10).Value = ticket.Clas;
            command.ExecuteNonQuery();
            DataBase.getInstance().closeConnection();
        }
    }
}
