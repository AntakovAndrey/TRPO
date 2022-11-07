﻿using Microsoft.Data.SqlClient;
using System.Data;
using TRPO.Database;


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
        public Role PassangerRole { set; get; }
        public string Email { set; get; }
        
        public Passanger() { 
            PassangerRole = new UserRole();
        }

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
            if(role==AdminRole.getRole())
            {
                PassangerRole = new AdminRole();
            }
            else
            {
                PassangerRole = new UserRole();
            }
            Email = email;
        }
        public string GetRole()
        {
            return PassangerRole.getRole();
        }
        public void SavePassangerToDB()
        {
            string commandExpression = "INSERT Passanger (Name, Surname, Pasport_ser, Pasport_num, Nationality, Date_of_birth, Telephone, Password, Role, Email)" +
                " VALUES (@Name, @Surname, @PassportSeries, @PassportNumber, @Nationality, @DateOfBirth, @Telephone, @Password, @Role, @Email)";
            SqlCommand command = new SqlCommand(commandExpression, DataBase.getInstance().GetConnection());
            command.Parameters.Add("@Name", System.Data.SqlDbType.NChar, 20).Value = Name;
            command.Parameters.Add("@Surname", System.Data.SqlDbType.NChar, 20).Value = Surname;
            command.Parameters.Add("@Password", System.Data.SqlDbType.NVarChar, 20).Value = Password;
            command.Parameters.Add("@PassportSeries", System.Data.SqlDbType.NChar, 2).Value = PassportSeries;
            command.Parameters.Add("@Nationality", System.Data.SqlDbType.NChar, 20).Value = Nationality;
            command.Parameters.Add("@DateOfBirth", System.Data.SqlDbType.Date).Value = DateOfBirth;
            command.Parameters.Add("@Telephone", System.Data.SqlDbType.NChar, 20).Value = Telephone;
            command.Parameters.Add("@PassportNumber", System.Data.SqlDbType.Int).Value = PassportNumber;
            command.Parameters.Add("@Role", System.Data.SqlDbType.NChar, 5).Value = GetRole();
            command.Parameters.Add("@Email", System.Data.SqlDbType.NVarChar, 50).Value = Email;
            DataBase.getInstance().openConnection();
            command.ExecuteNonQuery();
            DataBase.getInstance().closeConnection();
        }

        private static Passanger GetFromDBByCommand(SqlCommand command)
        {
            DataRow[] userInfo;
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(table);
            userInfo = table.Select();
            if(userInfo.Length > 0)
            {
                return new Passanger(
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
            return null;            
        }

        public static Passanger GetFromDBById(int id)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM Passanger WHERE Passanger_id = @id", DataBase.getInstance().GetConnection());
            command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;
            return GetFromDBByCommand(command);
        }
        public static Passanger GetFromDBByEmail(string email)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM Passanger WHERE Email = @userEmail", DataBase.getInstance().GetConnection());
            command.Parameters.Add("@userEmail", System.Data.SqlDbType.NVarChar,50).Value = email;
            return GetFromDBByCommand(command);
        }
        public static Passanger GetFromDBByEmailAndPassword(string email,string password)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM Passanger WHERE Email = @userEmail AND Password = @userPassword", DataBase.getInstance().GetConnection());
            command.Parameters.Add("@userEmail", System.Data.SqlDbType.NVarChar, 50).Value = email;
            command.Parameters.Add("@userPassword", System.Data.SqlDbType.NChar, 20).Value = password;
            return GetFromDBByCommand(command);
        }
    }
}