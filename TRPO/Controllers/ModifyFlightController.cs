using Microsoft.AspNetCore.Mvc;
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
        public IActionResult ModifyItem(int? id)
        {
            ViewBag.id = id;
            return View();
        }
        [HttpPost]
        public IActionResult ModifyItem(ModifyFlightViewModel modifiedFlight)
        {
            if(ModelState.IsValid)
            {

                //if(modifiedFlight.Status!=null)
                //{

                //}
                //if(modifiedFlight.Date!=null)
                //{

                //}
                //if(mod)
            }
            return RedirectToAction("ItemModified",flight);
        }
        public IActionResult ItemModified(Flight flight)
        {
            return View();
        }
    }
}
