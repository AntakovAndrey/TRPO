namespace TRPO.interfaces
{
    public interface IAllFlights
    {
        IEnumerable<Flight> Flights { get; } 
    }
}