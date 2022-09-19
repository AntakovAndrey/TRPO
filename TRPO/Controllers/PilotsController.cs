using Microsoft.AspNetCore.Mvc;

namespace TRPO.Controllers
{
    public class PilotsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
