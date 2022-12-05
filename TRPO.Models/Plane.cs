using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.SqlTypes;
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

    }
}