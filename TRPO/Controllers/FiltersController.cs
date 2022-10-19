using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace TRPO.Controllers
{
    public class FiltersController : Controller
    {
        //[Authorize(Policy ="")]        
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Resault()
        {
            FiltersBuilder filtersBuilder = new FiltersBuilder(DataBase.getInstance().GetConnection());
            filtersBuilder.SetFinishPoint(Request.Form["FinishPoint"]);
            filtersBuilder.SetFinishDate(Request.Form["FinishDate"]);
            filtersBuilder.SetStartDate(Request.Form["StartDate"]);
            var command = filtersBuilder.GetResault();
            ViewBag.command = command;
            return View();
        }
    }
}
