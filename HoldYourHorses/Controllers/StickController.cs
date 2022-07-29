using Microsoft.AspNetCore.Mvc;

namespace HoldYourHorses.Controllers
{
    public class StickController : Controller
    {
        public IActionResult Index()
        {
            //Min rad 9 är bättre än Lovisas? :D 
            return View();
        }
    }
}
