using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.SqlTypes;
using System.Security.Claims;
using TRPO.Models;

namespace TRPO.Controllers
{
    public class SignInController : Controller
    {
        DataRow[] userInfo;
        DataBase dataBase = DataBase.getInstance();

        public IActionResult Check()
        {
            if(ModelState.IsValid)
            {
                string Name = Request.Form["Name"];
                string Surname = Request.Form["Surname"];
                string Telephone = Request.Form["Telephone"];
                string Password = Request.Form["Password"];
                dataBase.openConnection();
                SqlCommand command = new SqlCommand("SELECT * FROM Passanger WHERE Name = @uN AND Surname = @uSN AND Telephone = @uT AND Password = @uP", dataBase.GetConnection());
                command.Parameters.Add("@uN", System.Data.SqlDbType.NChar).Value = Name;
                command.Parameters.Add("@uSN", System.Data.SqlDbType.NChar).Value = Surname;
                command.Parameters.Add("@uT", System.Data.SqlDbType.NChar).Value = Telephone;
                command.Parameters.Add("@uP", System.Data.SqlDbType.NChar).Value = Password;
                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(table);
                userInfo = table.Select();
                if (userInfo.Length > 0)
                {
                    Console.WriteLine("Вы вошли");
                    var claims = new List<Claim> { new Claim(ClaimTypes.MobilePhone, Telephone),new Claim(ClaimTypes.Name, Name), new Claim(ClaimTypes.Surname,Surname) };
                    // создаем объект ClaimsIdentity
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    // установка аутентификационных куки
                    //SignIn(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                    SignIn(new ClaimsPrincipal(claimsIdentity),CookieAuthenticationDefaults.AuthenticationScheme);
                    //await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                    var uN = User.FindFirst(ClaimsIdentity.DefaultNameClaimType);
                    Console.WriteLine(uN.Value);
                    return Redirect("/authorize");

                }
                else
                {
                    Console.WriteLine("Неправильный логин и пароль");
                    return Unauthorized();


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
