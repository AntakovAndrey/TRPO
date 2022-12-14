using TRPO.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TRPO.Controllers
{
    public class RouteController : Controller
    {
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Check(Models.Route route)
        {
            if(ModelState.IsValid)
            {
                RouteDB.SaveRouteToDB(route);
                return RedirectToAction("RouteAdded",route);
            }
            else
            {
                return View("Index", route);
            }
        }
        public IActionResult RouteAdded(Models.Route route)
        {
            return View(route);
        }
    }
}