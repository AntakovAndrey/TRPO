using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace TRPO.Controllers
{
    public class TicketsController : Controller
    {
        [Authorize(Policy = "")]
        public IActionResult Index()
        {
            int userId = Convert.ToInt32(HttpContext.User.FindFirstValue("id"));
            ViewBag.UserId = userId;
            return View();
        }
    }
}
