namespace TRPO.Models
{
    public class Passanger
    {
        public int PassangerId { set; get; }
        public string Name { set; get; }
        public string Surname { set; get; }
        public string PassportSeries { set; get; }
        public int PassportNumber { set; get; }
        public DateTime DateOfBirth { set; get; }
        public string Telephone { set; get; }
        public string Nationality { set; get; }
        public string Password { set; get; }
        Passanger(int passangerId, string name, string surname, string passportSeries, int passportNumber, DateTime dateOfBirth, string telephone, string nationality, string password)
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
        }
    }
    

}
