using Microsoft.Data.SqlClient;
using System;

namespace TRPO
{
    public static class DataBase
    {
        static string connectionString;
        static SqlConnection connection;
        public static async void Connect()
        {
            connectionString = "Server=localhost;Database=master;Trusted_Connection=True;";
            connection = new SqlConnection(connectionString);
            Console.WriteLine("Свойства подключения:");
            Console.WriteLine($"\tСтрока подключения: {connection.ConnectionString}");
            Console.WriteLine($"\tБаза данных: {connection.Database}");
            Console.WriteLine($"\tСервер: {connection.DataSource}");
            Console.WriteLine($"\tВерсия сервера: {connection.ServerVersion}");
            Console.WriteLine($"\tСостояние: {connection.State}");
            Console.WriteLine($"\tWorkstationld: {connection.WorkstationId}");
        }
        public static async void InsertUser()
        {
            Connect();
            string sqlExpression = "INSERT INTO Passanger (Name, Surname, Pasport_ser, Pasport_num, Nationality, Date_of_birth, Telephone, Password) VALUES ('Tom', 'Хуём', 'МР', 12345, 'СССР',05.05.2000,'+4554552234','хуец')";
            await connection.OpenAsync();
            SqlCommand command = new SqlCommand(sqlExpression, connection);
            int number = await command.ExecuteNonQueryAsync();
            //Console.WriteLine($"Добавлено объектов: {number}");
        }
    }
}
