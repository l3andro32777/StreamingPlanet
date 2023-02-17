using Microsoft.AspNetCore.Mvc;

namespace StreamingPlanet
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
