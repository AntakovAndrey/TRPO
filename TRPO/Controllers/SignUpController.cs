using Microsoft.AspNetCore.Mvc;

namespace TRPO.Controllers
{
    public class SignUpController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
