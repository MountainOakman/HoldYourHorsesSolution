using HoldYourHorses.Models;
using HoldYourHorses.Views.Sticks;
using Microsoft.AspNetCore.Mvc;

namespace HoldYourHorses.Controllers
{
    public class SticksController : Controller
    {
        private readonly DataService dataService;

        public SticksController(DataService dataService)
        {
            this.dataService = dataService;
        }
        [HttpGet("")]
        public async Task<IActionResult> IndexAsync()
        {
            IndexVM model = await dataService.GetIndexVMAsync();
            return View(model);
        }

        [HttpGet("Product/{artikelnr}")]
        public IActionResult Details(int artikelNr)
        {
            return View(dataService.GetDetailsVM(artikelNr));
        }

        [HttpGet("checkout")]
        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost("checkout")]
        //public IActionResult Checkout(CheckoutVM checkoutVM)
        //{
        //    if (!ModelState.IsValid)
        //        return View();
        //    else
        //        dataService.Checkout(checkoutVM);
        //    return RedirectToAction(nameof(Kvitto));
        //}

        [HttpGet("kvitto")]
        public IActionResult Kvitto()
        {
            return View();
        }
    
        public IActionResult UppdateraVarukorg(int antal, int artikelNr)
        {
            throw new NotImplementedException();
            //Uppdatera varukorg ajax från Details 
        }
    
    }
}
