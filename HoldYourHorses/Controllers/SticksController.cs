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

        [HttpGet("/uppdateravarukorg")]
        public IActionResult UppdateraVarukorg(int artikelNr, int antalVaror, string artikelnamn, decimal pris)
        {
            throw new NotImplementedException();
        }

        [HttpGet("IndexPartial")]
        public IActionResult IndexPartial(int minPrice, int maxPrice, int maxHK, int minHK, string typer, string materials, bool isAscending, string sortOn)
        {
            IndexPartialVM[] model = dataService.GetIndexPartial(minPrice, maxPrice, minHK, maxHK, typer, materials, isAscending, sortOn);
            return PartialView("_IndexPartial", model);
        }
    }
}
