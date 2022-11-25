namespace TRPO.Models
{
    public class Crew
    {
        public int CrewId { get; set; }
        public int FirstPilotId { get; set; }
        public int SecondPilotId { get; set; }
        public Crew(int crewId, int firstPilotId, int secondPilotId)
        {
            CrewId = crewId;
            FirstPilotId = firstPilotId;
            SecondPilotId = secondPilotId;
        }   
    }
}
