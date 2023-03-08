using Microsoft.AspNetCore.Mvc;

namespace StreamingPlanet.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
