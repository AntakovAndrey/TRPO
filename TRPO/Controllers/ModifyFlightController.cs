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
            ModifyFlightViewModel modifiedFlight = new ModifyFlightViewModel();
            modifiedFlight.FlightId = id;
            return View(modifiedFlight);
        }

        [HttpPost]
        public IActionResult ModifyItem(ModifyFlightViewModel modifiedFlight)
        {
            
            if(ModelState.IsValid)
            {
                FlightDB.UpdateFlight(modifiedFlight);
                var fff = FlightDB.getAllFlights();
                int i = 
                Flight f = fff.Where(f => f.FlightId == modifiedFlight.FlightId).First();

                //Flight flight = FlightDB.getAllFlights().Where(f=>f.FlightId==modifiedFlight.FlightId).First();
                return RedirectToAction("ItemModified", f);
            }
            return View();
        }

        public IActionResult ItemModified(Flight flight)
        {
            return View();
        }
    }
}