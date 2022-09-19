using Microsoft.AspNetCore.Mvc;

namespace TRPO.Controllers
{
    public class TicketsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
