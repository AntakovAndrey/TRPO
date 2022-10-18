using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Data;
using System.Security.Claims;

namespace TRPO.Controllers
{
    public class TicketsController : Controller
    {
        public IActionResult Index()
        {
            int userId = Convert.ToInt32(HttpContext.User.FindFirstValue("id"));
            ViewBag.UserId = userId;
            return View();
        }
    }
}
