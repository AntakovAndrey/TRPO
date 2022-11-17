using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
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
        public IActionResult Check(User user)
        {
            if (ModelState.IsValid)
            {
                if (TRPO.Models.User.GetFromDBByEmail(Request.Form["Email"]) == null)
                {
                    if(Request.Form["Password"] == Request.Form["Password_repeat"])
                    {
                        User passanger = new User();
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
                return Content($"{user.Name} - {user.Email}");
            }
            else
            {
                return View(user);
            }
            
        }
    }
}
