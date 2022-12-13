using Microsoft.AspNetCore.Mvc;
using TRPO.Database;
using TRPO.Models;

namespace TRPO.Controllers
{
    public class ModifyFlightController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ModifyItem(int id)
        {
            ViewBag.id = id;
            return View();
        }

        [HttpPost]
        public IActionResult ModifyItem(ModifyFlightViewModel modifiedFlight)
        {
            FlightDB.UpdateFlight(modifiedFlight);
            Flight flight = FlightDB.getAllFlights().Where(f=>f.FlightId==modifiedFlight.FlightId).First();
            return RedirectToAction("ItemModified", flight);    
        }

        public IActionResult ItemModified(Flight flight)
        {
            return View();
        }
    }
}