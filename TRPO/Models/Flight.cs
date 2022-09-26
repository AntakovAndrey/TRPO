namespace TRPO.Models
{
    public class Flight
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public TimeOnly Start_time { get; set; }
        public TimeOnly Finish_time { get; set; }
        public string Start_point { get; set; }
        public string Finish_point { get; set; }
        public int Palane_id { get; set; }
        public int Crew_id { get; set; }
    }
}
