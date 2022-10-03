using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient;
using System.Data.SqlTypes;
using TRPO.Models;

namespace TRPO.Controllers
{
    public class SignInController : Controller
    {
       


        public IActionResult Check()
        {
            if(ModelState.IsValid)
            {
                string Name = Request.Form["Name"];
                string Password = Request.Form["Password"];
                DataBase dataBase = new DataBase();
                dataBase.openConnection();
                SqlCommand command = new SqlCommand($"SELECT * FROM Passanger WHERE Name = {Name}");
                SqlDataReader reader = command.ExecuteReader();
                if(reader.HasRows)
                {
                    string dbPassword = reader.GetString(8);
                    if(Password==dbPassword)
                    {
                        Console.WriteLine("Пиздато");
                    }
                    else
                    {
                        Console.WriteLine("Почти пиздато");
                    }
                }
                else
                {
                    Console.WriteLine("хуёва");
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
