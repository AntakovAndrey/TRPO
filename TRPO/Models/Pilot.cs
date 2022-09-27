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
        Pilot(int pilotId, string name, string surname, string shedul, TimeOnly workHoursStart, TimeOnly workHoursFinsh)
        {
            PilotId = pilotId;
            Name = name;
            Surname = surname;
            Shedul = shedul;
            WorkHoursStart = workHoursStart;
            WorkHoursFinsh = workHoursFinsh;
        }
    }
}
