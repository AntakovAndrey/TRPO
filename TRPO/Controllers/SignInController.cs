using Microsoft.AspNetCore.Authentication;
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
        public async Task<IActionResult> Check()
        {
            if(ModelState.IsValid)
            {
                string Name = Request.Form["Name"];
                string Surname = Request.Form["Surname"];
                string Telephone = Request.Form["Telephone"];
                string Password = Request.Form["Password"];
                DataBase.getInstance().openConnection();
                SqlCommand command = new SqlCommand("SELECT * FROM Passanger WHERE Name = @uN AND Surname = @uSN AND Telephone = @uT AND Password = @uP", DataBase.getInstance().GetConnection());
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
                    var id = Convert.ToString(userInfo[0][0]);
                    var claims = new List<Claim> { new Claim("id",id),new Claim("Name", Name), new Claim(ClaimTypes.Surname,Surname) };
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);                   
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                    var l = HttpContext.User.FindFirstValue(ClaimsIdentity.DefaultIssuer);
                    //var x = HttpContext.User.FindFirstValue(ClaimsIdentity.DefaultNameClaimType);
                    var login = HttpContext.User.FindFirstValue("Name");
                    Console.WriteLine(login+ "\n"+ l);
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
