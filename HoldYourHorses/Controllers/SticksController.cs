﻿using HoldYourHorses.Models;
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

        public async Task<IActionResult> IndexAsync(string search)
        {
            IndexVM model = await dataService.GetIndexVMAsync(search);
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
            int numberOfProducts = dataService.AddToCart(artikelNr, antalVaror, artikelNamn, pris);
            return Content(numberOfProducts.ToString());
        }

        [HttpGet("/deleteItem")]
        public IActionResult Kassa(int artikelNr)
        {
            dataService.DeleteItem(artikelNr);
            return Kassa();
        }


        [HttpGet("IndexPartial")]
        public IActionResult IndexPartial(int minPrice, int maxPrice, int maxHK, int minHK, string typer, string materials, bool isAscending, string sortOn)
        {
            IndexPartialVM[] model = dataService.GetIndexPartial(minPrice, maxPrice, minHK, maxHK, typer, materials, isAscending, sortOn);
            return PartialView("_IndexPartial", model);
        }

		[HttpGet("Kassa")]
        public IActionResult Kassa()
		{
            KassaVM[] model = dataService.GetKassaVM();
            return View(model);
		}

        [HttpGet("rensakorg")]
        public IActionResult Kassa(string korg)
        {
            dataService.ClearCart();
            return Kassa();
        }
    }
}
