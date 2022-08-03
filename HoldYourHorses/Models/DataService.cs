using HoldYourHorses.Models.Entities;
using HoldYourHorses.Views.Sticks;
using Microsoft.EntityFrameworkCore;

namespace HoldYourHorses.Models
{
    public class DataService
    {
        private readonly SticksDBContext context;
        List<Stick> sticks = new List<Stick>();

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
                     Material = o.Material,
                     Typ = o.Typ,
                     Beskrivning = o.Beskrivning,
                     Tillverkningsland = o.Tillverkningsland,
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
            //Response.Cookies.Append("ShoppingCart");
            var sticks = await context.Sticks.Select(o => new
            {
                Artikelnamn = o.Artikelnamn,
                Pris = o.Pris,
                Artikelnr = o.Artikelnr,
                Hästkrafter = o.Hästkrafter,
                Material = o.Material,
                Typ = o.Typ
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
                Materialer = sticks.DistinctBy(o => o.Material).Select(o => o.Material).ToArray(),
                Typer = sticks.DistinctBy(o => o.Typ).Select(o => o.Typ).ToArray(),
                Cards = cards.ToArray()
            };
            return indexVM;
        }

        public void AddToCartCookie(List<ShoppingCartProduct> shoppingCartProducts)
        {
            //var cookie = Response.Cookies.Append("ShoppingCart", shoppingCartProducts, new CookieOptions { Expires = DateTime.Now.AddYears(1)});
        }

    }
}
