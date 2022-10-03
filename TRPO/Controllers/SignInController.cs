using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.SqlTypes;
using TRPO.Models;

namespace TRPO.Controllers
{
    public class SignInController : Controller
    {
        DataRow[] userInfo;
        DataBase dataBase = new DataBase();

        public IActionResult Check()
        {
            if(ModelState.IsValid)
            {
                string Name = Request.Form["Name"];
                string Password = Request.Form["Password"];
                dataBase.openConnection();
                SqlCommand command = new SqlCommand("SELECT * FROM Passanger WHERE Name = @uN AND Password = @uP",dataBase.GetConnection());
                command.Parameters.Add("@uN", System.Data.SqlDbType.NChar).Value = Name;
                command.Parameters.Add("@uP", System.Data.SqlDbType.NChar).Value = Password;
                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(table);
                userInfo = table.Select();
                if (userInfo.Length>0)
                {
                    Console.WriteLine("Вы вошли");
                }
                else
                {
                    Console.WriteLine("Неправильный логин и пароль");
                }
            }
            return View("Index");
        }
       
        public IActionResult Index()
        {
            return View();
        }

    }
}
