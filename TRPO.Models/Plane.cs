using Microsoft.Data.SqlClient;
using System.Data;
using TRPO.Services;

namespace TRPO.Models
{
    public class Plane
    {
        public int PlaneId { get; set; }
        public string Type { get; set; }
        public int MaxFlightRange { get; set; }
        public int NumberOfSeats { get; set; }
        public double FuelConsumtion { get; set; }
        public Plane(int planeId, string type, int maxFlightRange, int numberOfSeats, double fuelConsumtion)
        {
            PlaneId = planeId;
            Type = type;
            MaxFlightRange = maxFlightRange;
            NumberOfSeats = numberOfSeats;
            FuelConsumtion = fuelConsumtion;
        }
        public Plane() { }

        public static Plane GetFromDBById(int id)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM Plane WHERE Plane_id = @id", DataBase.getInstance().getConnection());
            command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;
            return GetFromDBByCommand(command);
        }


        private static Plane GetFromDBByCommand(SqlCommand command)
        {
            DataRow[] planeInfo;
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(table);
            planeInfo = table.Select();
            if (planeInfo.Length > 0)
            {
                return new Plane(
                    Convert.ToInt32(planeInfo[0][0]),
                    Convert.ToString(planeInfo[0][1]),
                    Convert.ToInt32(planeInfo[0][2]),
                    Convert.ToInt32(planeInfo[0][3]),
                    Convert.ToDouble(planeInfo[0][4])
                );
            }
            else throw new Exception("Самолет с таким номером не найден.") ;
        }
    }
}