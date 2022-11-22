using Microsoft.Data.SqlClient;
using System.Data;
using TRPO.Services;
using TRPO.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
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

        public IRole Role { private set; get; }


        public User()
        {
            Role = new UserRole();
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
                Role = new AdminRole();
            }
            else
            {
                Role = new UserRole();
            }
            Email = email;
        }

        public void SaveUserToDB()
        {
            string commandExpression = "INSERT [User] (Name, Surname, Pasport_ser, Pasport_num, Nationality, Date_of_birth, Telephone, Password, Role, Email)" +
                " VALUES (@Name, @Surname, @PassportSeries, @PassportNumber, @Nationality, @DateOfBirth, @Telephone, @Password, @Role, @Email)";
            SqlCommand command = new SqlCommand(commandExpression, DataBase.getInstance().getConnection());
            command.Parameters.Add("@Name", System.Data.SqlDbType.NChar, 20).Value = Name;
            command.Parameters.Add("@Surname", System.Data.SqlDbType.NChar, 20).Value = Surname;
            command.Parameters.Add("@Password", System.Data.SqlDbType.NVarChar, 20).Value = HashPassword(Password);
            command.Parameters.Add("@PassportSeries", System.Data.SqlDbType.NChar, 2).Value = PassportSeries;
            command.Parameters.Add("@Nationality", System.Data.SqlDbType.NChar, 20).Value = Nationality;
            command.Parameters.Add("@DateOfBirth", System.Data.SqlDbType.Date).Value = DateOfBirth;
            command.Parameters.Add("@Telephone", System.Data.SqlDbType.NChar, 20).Value = Telephone;
            command.Parameters.Add("@PassportNumber", System.Data.SqlDbType.Int).Value = PassportNumber;
            command.Parameters.Add("@Role", System.Data.SqlDbType.NChar, 5).Value = Role.Name;
            command.Parameters.Add("@Email", System.Data.SqlDbType.NVarChar, 50).Value = Email;
            DataBase.getInstance().openConnection();
            command.ExecuteNonQuery();
            DataBase.getInstance().closeConnection();
        }

        public static User GetFromDBById(int id)
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
            SqlCommand command = new SqlCommand("SELECT * FROM [User] WHERE Email = @userEmail AND Password = @userPassword", DataBase.getInstance().getConnection());
            command.Parameters.Add("@userEmail", System.Data.SqlDbType.NVarChar, 50).Value = email;
            command.Parameters.Add("@userPassword", System.Data.SqlDbType.NVarChar, 50).Value = password;
            return GetFromDBByCommand(command);
        }

        private static User GetFromDBByCommand(SqlCommand command)
        { 
            DataRow[] userInfo;
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(table);
            userInfo = table.Select();
            if (userInfo.Length > 0)
            {
                return new User(
                    passangerId: Convert.ToInt32(userInfo[0][0]),
                    name: Convert.ToString(userInfo[0][1]),
                    surname: Convert.ToString(userInfo[0][2]),
                    passportSeries: Convert.ToString(userInfo[0][3]),
                    passportNumber: Convert.ToInt32(userInfo[0][4]),
                    dateOfBirth: Convert.ToDateTime(userInfo[0][6]),
                    telephone: Convert.ToString(userInfo[0][7]),
                    nationality: Convert.ToString(userInfo[0][5]),
                    password: Convert.ToString(userInfo[0][8]),
                    role: Convert.ToString(userInfo[0][9]),
                    email: Convert.ToString(userInfo[0][10])
                );
            }
            else return null;
        }

        private static string HashPassword(string password)
        {
            byte[] salt;
            byte[] buffer;
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }

    }
}