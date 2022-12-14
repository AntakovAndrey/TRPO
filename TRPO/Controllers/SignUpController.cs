using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using TRPO.Database;
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
                
                if ((UserDB.GetFromDBByEmail(Request.Form["Email"]) == null) && (Request.Form["Password"] == Request.Form["Password_repeat"]))
                {
                    UserDB.SaveUserToDB(user);
                    return Redirect("../Home");
                }
                else
                {
                    return View(user);
                }
            }
            else
            {
                return View(user);
            }    
        }
    }
}