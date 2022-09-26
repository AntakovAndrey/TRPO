namespace TRPO.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public int Passanger_Id { get; set; }
        public int Flight_Id { get; set; }
        public double Price { get; set; }
        public string Class { get; set; }
    }
}
