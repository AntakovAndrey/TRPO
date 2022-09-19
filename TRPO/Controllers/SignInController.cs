using Microsoft.AspNetCore.Mvc;

namespace TRPO.Controllers
{
    public class SignInController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
