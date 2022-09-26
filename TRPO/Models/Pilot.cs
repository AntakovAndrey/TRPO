namespace TRPO.Models
{
    public class Pilot
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Shedul { get; set; }
        public TimeOnly Work_Hours_start { get; set; }
        public TimeOnly Work_Hours_finsh { get; set; }
    }
}
