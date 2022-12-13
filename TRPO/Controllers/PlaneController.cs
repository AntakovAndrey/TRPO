using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TRPO.Database;

namespace TRPO.Controllers
{
    public class PlaneController : Controller
    {
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Check(Models.Plane plane)
        {
            if (ModelState.IsValid)
            {
                PlaneDB.SavePlaneToDB(plane);
                return RedirectToAction("PlaneAdded", plane);
            }
            else
            {
                return View("Index", plane);
            }
        }
        public IActionResult PlaneAdded(Models.Plane plane)
        {
            return View(plane);
        }
    }
}