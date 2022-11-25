namespace TRPO.Models
{
    public class Pilot
    {
        public int PilotId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public Pilot(int pilotId, string name, string surname)
        {
            PilotId = pilotId;
            Name = name;
            Surname = surname;
        }
        public Pilot() { }
    }
}
