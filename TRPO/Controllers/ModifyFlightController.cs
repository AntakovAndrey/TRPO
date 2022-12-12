using Microsoft.AspNetCore.Mvc;

namespace TRPO.Controllers
{
    public class ModifyFlightController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ModifyItem(int? id)
        {
            ViewBag.id = id;
            return View();
        }
    }
}
