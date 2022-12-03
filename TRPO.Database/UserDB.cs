using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TRPO.Services;

namespace TRPO.Database
{
    internal class UserDB
    {
        public int PassangerId { get; }

        [Required(ErrorMessage = "Имя не введено")]
        [StringLength(50, ErrorMessage = "Имя слишком длинное.")]
        [MinLength(1, ErrorMessage = "Имя слишком короткое.")]
        [RegularExpression(@"([А-Я]{1}[а-яё]{1,23})|([A-Z]{1}[a-z]{1-23})", ErrorMessage = "Имя введено неправильно.")]
        public string Name { set; get; }

        [Required(ErrorMessage = "Фамилия не введена")]
        [StringLength(50, ErrorMessage = "Фамилия слишком длинная.")]
        [MinLength(1, ErrorMessage = "Фамилия слишком короткое.")]
        [RegularExpression(@"([А-Я]{1}[а-яё]{1,23})|([A-Z]{1}[a-z]{1-23})", ErrorMessage = "Фамилия введена неправильно.")]
        public string Surname { set; get; }

        [Required(ErrorMessage = "Серия паспорта не введена.")]
        [RegularExpression(@"(MC)|(MP)", ErrorMessage = "Некорректная серия паспорта.")]
        public string PassportSeries { set; get; }

        [Required(ErrorMessage = "Номер паспорта не введен.")]
        public int PassportNumber { set; get; }

        [Required(ErrorMessage = "Дата рождения не введена.")]
        public DateTime DateOfBirth { set; get; }

        [Required(ErrorMessage = "Номер телефона не введен.")]
        [Phone(ErrorMessage = "Некорректный номер.")]
        public string Telephone { set; get; }

        [Required(ErrorMessage = "Гражданство не введено.")]
        public string Nationality { set; get; }

        [Required(ErrorMessage = "Пароль не введен.")]
        public string Password { set; get; }

        [Required(ErrorMessage = "Адрес не введен.")]
        [EmailAddress(ErrorMessage = "Некорректный адрес.")]
        public string Email { set; get; }

        public string Role { private set; get; }
        public static UserDB GetFromDBById(int id)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM [User] WHERE Passanger_id = @id", DataBase.getInstance().getConnection());
            command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;
            return GetFromDBByCommand(command);
        }
        public static User GetFromDBByEmail(string email)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM [User] WHERE Email = @userEmail", DataBase.getInstance().getConnection());
            command.Parameters.Add("@userEmail", System.Data.SqlDbType.NVarChar, 50).Value = email;
            return GetFromDBByCommand(command);
        }
        public static User GetFromDBByEmailAndPassword(string email, string password)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM [User] WHERE Email = @userEmail", DataBase.getInstance().getConnection());
            command.Parameters.Add("@userEmail", System.Data.SqlDbType.NVarChar, 50).Value = email;
            command.Parameters.Add("@userPassword", System.Data.SqlDbType.NVarChar, 50).Value = password;
            try
            {
                var tmpUser = GetFromDBByCommand(command);
                if (VerifyHashedPassword(tmpUser.Password, password))
                {
                    return tmpUser;
                }
                else
                {
                    throw new ArgumentException("Неверное имя пользователя или пароль.");
                }
            }
            catch (NullReferenceException)
            {
                throw new ArgumentException("Неверное имя пользователя или пароль.");
            }
        }
        public void SaveUserToDB(UserDB user)
        {
            string commandExpression = "INSERT [User] (Name, Surname, Pasport_ser, Pasport_num, Nationality, Date_of_birth, Telephone, Password, Role, Email)" +
                " VALUES (@Name, @Surname, @PassportSeries, @PassportNumber, @Nationality, @DateOfBirth, @Telephone, @Password, @Role, @Email)";
            SqlCommand command = new SqlCommand(commandExpression, DataBase.getInstance().getConnection());
            command.Parameters.Add("@Name", System.Data.SqlDbType.NChar, 20).Value = user.Name;
            command.Parameters.Add("@Surname", System.Data.SqlDbType.NChar, 20).Value = user.Surname;
            command.Parameters.Add("@Password", System.Data.SqlDbType.NVarChar, 100).Value = user.HashPassword(Password);
            command.Parameters.Add("@PassportSeries", System.Data.SqlDbType.NChar, 2).Value = user.PassportSeries;
            command.Parameters.Add("@Nationality", System.Data.SqlDbType.NChar, 20).Value = user.Nationality;
            command.Parameters.Add("@DateOfBirth", System.Data.SqlDbType.Date).Value = user.DateOfBirth;
            command.Parameters.Add("@Telephone", System.Data.SqlDbType.NChar, 20).Value = user.Telephone;
            command.Parameters.Add("@PassportNumber", System.Data.SqlDbType.Int).Value = user.PassportNumber;
            command.Parameters.Add("@Role", System.Data.SqlDbType.NChar, 5).Value = user.Role;
            command.Parameters.Add("@Email", System.Data.SqlDbType.NVarChar, 50).Value = user.Email;
            DataBase.getInstance().openConnection();
            command.ExecuteNonQuery();
            DataBase.getInstance().closeConnection();
        }
    }
}
