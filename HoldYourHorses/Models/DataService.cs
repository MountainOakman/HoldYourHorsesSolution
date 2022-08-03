using HoldYourHorses.Models.Entities;
using HoldYourHorses.Views.Sticks;
using Microsoft.EntityFrameworkCore;

namespace HoldYourHorses.Models
{
    public class DataService
    {
        private readonly SticksDBContext context;

        public DataService(SticksDBContext context)
        {
            this.context = context;
        }

        internal DetailsVM GetDetailsVM(int artikelNr)
        {
            return context.Sticks
                 .Where(o => o.Artikelnr == artikelNr)
                 .Select(o => new DetailsVM()
                 {
                     Artikelnr = o.Artikelnr,
                     Pris = o.Pris,
                     Hästkrafter = o.Hästkrafter,
                     Trädensitet = o.Trädensitet,
                     Artikelnamn = o.Artikelnamn,
                     Material = o.Material.Namn,
                     Kategori = o.Kategori.Namn,
                     Beskrivning = o.Beskrivning,
                     Tillverkningsland = o.Tillverkningsland.Namn,
                     AbsBroms = o.AbsBroms,
                 })
                 .Single();
                
        //TODO:Tilldela prop :public string Bild { get; set; }
            
        }

        internal CheckoutVM Checkout(CheckoutVM checkoutVM)
        {
            return new CheckoutVM()
            {
                FirstName = checkoutVM.FirstName,
                LastName = checkoutVM.LastName,
                Email = checkoutVM.Email,
                Address = checkoutVM.Address,
                City = checkoutVM.City,
                ZipCode = checkoutVM.ZipCode,
                Country = checkoutVM.Country,
            };
        }

        internal async Task<IndexVM> GetIndexVMAsync()
        {
            var sticks = await context.Sticks.Select(o => new
            {
                Artikelnamn = o.Artikelnamn,
                Pris = o.Pris,
                Artikelnr = o.Artikelnr,
                Hästkrafter = o.Hästkrafter,
                Material = o.Material,
                Kategori = o.Kategori.Namn
            }).ToArrayAsync(); //fixa filtreringen innan toArrayAsync!
            var cards = sticks.Select(o => new IndexVM.Card()
            {
                Namn = o.Artikelnamn,
                Pris = o.Pris,
                ArtikelNr = o.Artikelnr,
            });
            var indexVM = new IndexVM
            {
                PrisMax = sticks.Max(o => o.Pris),
                PrisMin = sticks.Min(o => o.Pris),
                HästkrafterMax = sticks.Max(o => o.Hästkrafter),
                HästkrafterMin = sticks.Min(o => o.Hästkrafter),
                Materialer = sticks.DistinctBy(o => o.Material.Namn).Select(o => o.Material.Namn).ToArray(),
                Kategorier = sticks.DistinctBy(o => o.Kategori).Select(o => o.Kategori).ToArray(),
                Cards = cards.ToArray()
            };
            return indexVM;
        }
	}
}
