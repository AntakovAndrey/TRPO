using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TRPO.Controllers
{
    public class AddingFlightController : Controller
    {
        public IActionResult Index()
        {
            return View("Index");
            ViewBag.Planes = new SelectList(TRPO.Models.Plane.getAllPlanes(), "PlaneId","Type");
        }
        public IActionResult Check(TRPO.Models.Flight flight)
        {
            if (ModelState.IsValid)
            {
                flight.saveFlightToDB();
                ViewBag.Message = "Новый рейс добавлен.";
            }
            return View("Index",flight);
        }
    }
}