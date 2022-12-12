
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TRPO.Controllers
{
    public class RouteController : Controller
    {
        [Authorize(Roles = "User")]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "User")]
        public IActionResult Check(TRPO.Models.Route route)
        {
            if(ModelState.IsValid)
            {
                route.saveToDB();
                ViewBag.Message = "Новый маршрут успешно добавлен.";
                return View("Index",route);
            }
            else
            {
                return View("Index", route);
            }
            
        }
    }
}
