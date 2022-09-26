namespace TRPO.Models
{
    public class Pilot
    {
        public int PilotId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Shedul { get; set; }
        public TimeOnly WorkHoursStart { get; set; }
        public TimeOnly WorkHoursFinsh { get; set; }
    }
}
