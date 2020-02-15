using Microsoft.AspNetCore.Mvc;

namespace Web22.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
