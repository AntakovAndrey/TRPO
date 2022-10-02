
namespace TRPO.Models
{
    public class Pilot
    {
        public int PilotId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Shedule { get; set; }
        public TimeOnly WorkHoursStart { get; set; }
        public TimeOnly WorkHoursFinish { get; set; }

        public Pilot(int pilotId, string name, string surname, string shedule, TimeOnly workHoursStart, TimeOnly workHoursFinish)
        {
            PilotId = pilotId;
            Name = name;
            Surname = surname;
            Shedule = shedule;
            WorkHoursStart = workHoursStart;
            WorkHoursFinish = workHoursFinish;
        }
    }
}
