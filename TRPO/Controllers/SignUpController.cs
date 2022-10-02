using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using TRPO.Models;

namespace TRPO.Controllers
{
    public class SignUpController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Check(Passanger passanger)
        {
            //string name = Request.mo
            string name = Request.Form["Name"];
            Console.WriteLine(name);
            Console.WriteLine(ModelState);
            if (ModelState.IsValid)
            {
                //Index().
                //string commandExpression = $"INSERT INTO Passanger (Name,Surname,Pasport_ser,Pasport_num,Nationality,Date_of_birth,Telephone,Password) VALUES ({})";
                //DataBase dataBase = new DataBase();
                //dataBase.openConnection();
                //SqlCommand command = new SqlCommand()

                //Console.WriteLine(passanger.Surname);
                //string name = Request.Form["Name"];
                //Console.WriteLine(name);
                return Redirect("Index");
            }
            return View("Index");
        }
    }
}
