using Microsoft.AspNetCore.Mvc;

namespace TRPO.Controllers
{
    public class FiltersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
