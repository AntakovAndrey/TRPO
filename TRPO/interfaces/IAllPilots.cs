using TRPO.Models;

namespace TRPO.interfaces
{
    public interface IAllPilots
    {
        IEnumerable<Pilot> Pilots { get; }
    }
}
