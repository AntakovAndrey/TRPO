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
        public IActionResult Check()
        {
            if (ModelState.IsValid)
            {
                if (Passanger.GetFromDBByEmail(Request.Form["Email"]) == null)
                {
                    if(Request.Form["Password"] == Request.Form["Password_repeat"])
                    {
                        Passanger passanger = new Passanger();
                        passanger.Name = Request.Form["Name"];
                        passanger.Surname = Request.Form["Surname"];
                        passanger.Email = Request.Form["Email"];
                        passanger.Telephone = Request.Form["Telephone"];
                        passanger.PassportSeries = Request.Form["PassportSeries"];
                        passanger.PassportNumber = Convert.ToInt32(Request.Form["PassportNumber"]);
                        passanger.DateOfBirth = Convert.ToDateTime(Request.Form["DateOfBirth"]);
                        passanger.Nationality = Request.Form["Nationality"];
                        passanger.Password = Request.Form["Password"];
                        passanger.SavePassangerToDB();
                    }
                }
                DataBase.getInstance().closeConnection();
            }
            return View("Index");
        }
    }
}
