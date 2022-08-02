﻿using HoldYourHorses.Models;
using Microsoft.AspNetCore.Mvc;

namespace HoldYourHorses.Controllers
{
    public class StickController : Controller
    {
        private readonly DataService dataService;

        public StickController(DataService dataService)
        {
            this.dataService = dataService;
        }
        [HttpGet("")]
        public IActionResult Index()
        {
            return Content("hej");
            //ser alla detta?
        }
        [HttpGet("Product/{artikelnr}")]
        public IActionResult Details(int artikelNr)
        {
            return View(dataService.GetDetailsVM(artikelNr));
        }

      
    }
}