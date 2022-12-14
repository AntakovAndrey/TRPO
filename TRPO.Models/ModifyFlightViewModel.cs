using System.ComponentModel.DataAnnotations;

namespace TRPO.Models
{
    public class ModifyFlightViewModel
    {
        //[Required]
        public int FlightId { get; set; }

        public string Status { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan FinishTime { get; set; }

        public ModifyFlightViewModel() { }
    }
}
