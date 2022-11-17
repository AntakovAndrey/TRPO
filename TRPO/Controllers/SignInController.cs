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
                User passanger = TRPO.Models.User.GetFromDBByEmailAndPassword(Request.Form["Email"], Request.Form["Password"]);
                
                if (passanger!=null)
                {
                    
                    var claims = new List<Claim> {
                        new Claim("id", Convert.ToString(passanger.PassangerId)),
                        new Claim("Name", passanger.Name),
                        new Claim("Role", passanger.Role.Name),
                        //new Claim(ClaimTypes.Role,passanger.GetRole())
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
