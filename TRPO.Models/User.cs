using Microsoft.Data.SqlClient;
using System.Data;
using TRPO.Services;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace TRPO.Models
{
    public class User
    {   
        public int PassangerId { get; }

        [Required(ErrorMessage ="Имя не введено")]
        [StringLength(50,ErrorMessage ="Имя слишком длинное.")]
        [MinLength(1,ErrorMessage ="Имя слишком короткое.")]
        [RegularExpression(@"([А-Я]{1}[а-яё]{1,23})|([A-Z]{1}[a-z]{1-23})",ErrorMessage ="Имя введено неправильно.")]
        public string Name { set; get; }

        [Required(ErrorMessage = "Фамилия не введена")]
        [StringLength(50, ErrorMessage = "Фамилия слишком длинная.")]
        [MinLength(1, ErrorMessage = "Фамилия слишком короткое.")]
        [RegularExpression(@"([А-Я]{1}[а-яё]{1,23})|([A-Z]{1}[a-z]{1-23})", ErrorMessage = "Фамилия введена неправильно.")]
        public string Surname { set; get; }

        [Required(ErrorMessage ="Серия паспорта не введена.")]
        [RegularExpression(@"(MC)|(MP)",ErrorMessage ="Некорректная серия паспорта.")]
        public string PassportSeries { set; get; }

        [Required(ErrorMessage ="Номер паспорта не введен.")]
        public int PassportNumber { set; get; }

        [Required(ErrorMessage ="Дата рождения не введена.")]
        public DateTime DateOfBirth { set; get; }

        [Required(ErrorMessage ="Номер телефона не введен.")]
        [Phone(ErrorMessage ="Некорректный номер.")]
        public string Telephone { set; get; }

        [Required(ErrorMessage ="Гражданство не введено.")]
        public string Nationality { set; get; }

        [Required(ErrorMessage ="Пароль не введен.")]
        public string Password { set; get; }

        [Required(ErrorMessage ="Адрес не введен.")]
        [EmailAddress(ErrorMessage = "Некорректный адрес.")]
        public string Email { set; get; }

        public string Role { private set; get; }

        public User()
        {
            Role = UserRole.Name;
        }

        public User(int passangerId, string name, string surname, string passportSeries, int passportNumber, DateTime dateOfBirth, string telephone, string nationality, string password, string role, string email)
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
            if (role == AdminRole.Name)
            {
                Role = AdminRole.Name;
            }
            else
            {
                Role = UserRole.Name;
            }
            Email = email;
        }
    }
}