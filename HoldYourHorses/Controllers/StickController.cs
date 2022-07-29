using Microsoft.AspNetCore.Mvc;

namespace HoldYourHorses.Controllers
{
    public class StickController : Controller
    {
        public IActionResult Index()
        {
            return Content("hej");
            //Min rad 9 är bättre än Lovisas? :D 
        }
    }
}
