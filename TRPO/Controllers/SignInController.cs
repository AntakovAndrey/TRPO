using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TRPO.Models;
using TRPO.Interfaces;

namespace TRPO.Controllers
{
    public class SignInController : Controller
    {
        public async Task<IActionResult> Check()
        {
            if(ModelState.IsValid)
            {
                User user = Models.User.GetFromDBByEmailAndPassword(Request.Form["Email"], Request.Form["Password"]);
                
                if (user!=null)
                {
                    var claims = new List<Claim> { 
                        new Claim("id", Convert.ToString(user.PassangerId)),
                        new Claim("Name", user.Name),
                        new Claim("Role", user.Role)
                    };
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                    return Redirect("../Home");
                }
                else
                {
                    ViewBag.Message = "Неверный логин и пароль";
                    return View("Index");
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
