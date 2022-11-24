using Microsoft.AspNetCore.Mvc;

namespace TRPO.Controllers
{
    public class AddingFlightController : Controller
    {
        public IActionResult Index()
        {
            return View("Index");
        }
        public IActionResult Check(TRPO.Models.Flight flight)
        {
            if (ModelState.IsValid)
            {
                flight.saveFlightToDB();
            }
            return View("Index");
        }
    }
}
