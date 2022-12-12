using System.Data;
using TRPO.Models;
using Microsoft.Data.SqlClient;
using System.Security.Cryptography;

namespace TRPO.Database
{
    public class UserDB
    {
        public static User VerifyUser(string email, string password)
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
                    throw new ArgumentException();
                }
            }
            catch (NullReferenceException)
            {
                throw new ArgumentException("Неверное имя пользователя или пароль.");
            }
            catch (ArgumentException)
            {
                throw new ArgumentException("Неверное имя пользователя или пароль.");
            }
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
        
        public static void SaveUserToDB(User user)
        {
            string commandExpression = "INSERT [User] (Name, Surname, Pasport_ser, Pasport_num, Nationality, Date_of_birth, Telephone, Password, Role, Email)" +
                " VALUES (@Name, @Surname, @PassportSeries, @PassportNumber, @Nationality, @DateOfBirth, @Telephone, @Password, @Role, @Email)";
            SqlCommand command = new SqlCommand(commandExpression, DataBase.getInstance().getConnection());
            command.Parameters.Add("@Name", System.Data.SqlDbType.NChar, 20).Value = user.Name;
            command.Parameters.Add("@Surname", System.Data.SqlDbType.NChar, 20).Value = user.Surname;
            command.Parameters.Add("@Password", System.Data.SqlDbType.NVarChar, 100).Value = HashPassword(user.Password);
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

        private static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            byte[] buffer4;
            if (hashedPassword == null)
            {
                return false;
            }
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            byte[] src = Convert.FromBase64String(hashedPassword);
            if ((src.Length != 0x31) || (src[0] != 0))
            {
                return false;
            }
            byte[] dst = new byte[0x10];
            Buffer.BlockCopy(src, 1, dst, 0, 0x10);
            byte[] buffer3 = new byte[0x20];
            Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, dst, 0x3e8))
            {
                buffer4 = bytes.GetBytes(0x20);
            }
            return buffer3.SequenceEqual(buffer4);
        }

    }
}
