using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TRPO.Database;

namespace TRPO.Controllers
{
    public class AddingFlightController : Controller
    {
        public IActionResult Index()
        {
            return View("Index");
        }
        public IActionResult Check(Models.Flight flight)
        {
            flight.FlightRoute = RouteDB.getAllRoutes().Where(r=>r.Id==flight.RouteId).First();
            flight.Plane = PlaneDB.getAllPlanes().Where(plane=>plane.PlaneId==flight.PlaneId).First();
            flight.Pilot = PilotDB.getAllPilots().Where(pilot=>pilot.PilotId==flight.PilotId).First();
            if (ModelState.IsValid)
            {
                FlightDB.SaveFlightToDB(flight);
                return RedirectToAction("FlightAdded", flight);
            }
            return View("Index",flight);
        }
        public IActionResult FlightAdded(Models.Flight flight)
        {
            ViewBag.flight = flight;
            return View("Flight Added");
        }
    }
}