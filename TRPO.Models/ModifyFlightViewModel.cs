using System.ComponentModel.DataAnnotations;

namespace TRPO.Models
{
    public class ModifyFlightViewModel
    {
        [Required]
        public int FlightId { get; set; }

        public string Status { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly FinishTime { get; set; }

        public ModifyFlightViewModel() { }
    }
}
