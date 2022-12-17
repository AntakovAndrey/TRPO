using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TRPO.Controllers
{
    public class SelectFlight : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CheckFilters()
        {
            
            return RedirectToAction("Resault");
        }

        public IActionResult Resault()
        {
            return View();
        }               
    }
}