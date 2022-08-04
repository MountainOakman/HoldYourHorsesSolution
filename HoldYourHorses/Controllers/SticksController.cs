using HoldYourHorses.Models;
using HoldYourHorses.Views.Sticks;
using Microsoft.AspNetCore.Mvc;
using HoldYourHorses.Views.Shared;

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
        public IActionResult Checkout(CheckoutVM checkoutVM)
        {
            if (!ModelState.IsValid)
                return View();
            else
                dataService.Checkout(checkoutVM);
            return RedirectToAction(nameof(Kvitto));
        }

        [HttpGet("kvitto")]
        public IActionResult Kvitto()
        {
            return View();
        }

        [HttpGet("/uppdateravarukorg/")]
        public IActionResult Details(int artikelNr, int antalVaror, string artikelNamn, string price)
        {
            var p = price.ToCharArray()
            .Where(c => !Char.IsWhiteSpace(c))
            .Select(c => c.ToString())
            .Aggregate((a, b) => a + b);

            var pris = int.Parse(p);
            dataService.AddToCart(artikelNr, antalVaror, artikelNamn, pris);
            var pris = decimal.Parse(price);
            int numberOfProducts = dataService.AddToCart(artikelNr, antalVaror, artikelNamn, pris);
            //return Content(dataService.GetCart());
            return Content(numberOfProducts.ToString());
            //Uppdatera varukorg ajax från Details 
        }

        [HttpGet("IndexPartial")]
        public IActionResult IndexPartial(int minPrice, int maxPrice, int maxHK, int minHK, string typer, string materials, bool isAscending, string sortOn, string searchString)
        {
            IndexPartialVM[] model = dataService.GetIndexPartial(minPrice, maxPrice, minHK, maxHK, typer, materials, isAscending, sortOn, searchString);
            return PartialView("_IndexPartial", model);
        }
    }
}
