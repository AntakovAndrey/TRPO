namespace TRPO.Models
{
    public class Passanger
    {
        public int PassangerId { get; }
        public string Name { set; get; }
        public string Surname { set; get; }
        public string PassportSeries { set; get; } 
        public int PassportNumber { set; get; }
        public DateTime DateOfBirth { set; get; }
        public string Telephone { set; get; } 
        public string Nationality { set; get; }
        public string Password { set; get; }
        public string Role { set; get; }
        public string Email { set; get; }
        
        public Passanger() { }

        public Passanger(int passangerId, string name, string surname, string passportSeries, int passportNumber, DateTime dateOfBirth, string telephone, string nationality, string password, string role, string email)
        {
            PassangerId = passangerId;
            Name = name;
            Surname = surname;
            PassportSeries = passportSeries;
            PassportNumber = passportNumber;
            DateOfBirth = dateOfBirth;
            Telephone = telephone;
            Nationality = nationality;
            Password = password;
            Role = role;
            Email = email;
        }
    }
    

}
