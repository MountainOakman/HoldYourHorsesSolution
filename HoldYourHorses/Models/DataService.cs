using HoldYourHorses.Models.Entities;
using HoldYourHorses.Views.Shared;
using HoldYourHorses.Views.Sticks;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace HoldYourHorses.Models
{
    public class DataService
    {
        private readonly SticksDBContext context;

        public IHttpContextAccessor Accessor { get; }

        public DataService(SticksDBContext context, IHttpContextAccessor accessor)
        {
            this.context = context;
            Accessor = accessor;
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

        internal void AddToCart(int artikelNr, int antalVaror, string arikelNamn, decimal pris)
        {
            List<ShoppingCartProduct> products;

            var newItem = new ShoppingCartProduct()
            {
                Artikelnamn = arikelNamn,
                Pris = pris,
                ArtikelNr = artikelNr,
                Antal = antalVaror
            };
            if (string.IsNullOrEmpty(Accessor.HttpContext.Request.Cookies["ShoppingCart"]))
            {
                products = new List<ShoppingCartProduct>();
                products.Add(newItem);

                string json = JsonSerializer.Serialize(products);

                Accessor.HttpContext.Response.Cookies.Append("ShoppingCart", json);
            }
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
                Typ = o.Kategori.Namn
            }).ToArrayAsync();

            var indexVM = new IndexVM
            {
                PrisMax = Decimal.ToInt32(sticks.Max(o => o.Pris)),
                PrisMin = Decimal.ToInt32(sticks.Min(o => o.Pris)),
                HästkrafterMax = sticks.Max(o => o.Hästkrafter),
                HästkrafterMin = sticks.Min(o => o.Hästkrafter),
                Materialer = sticks.DistinctBy(o => o.Material.Namn).Select(o => o.Material.Namn).ToArray(),
                Kategorier = sticks.DistinctBy(o => o.Typ).Select(o => o.Typ).ToArray(),
            };
            return indexVM;
        }

        internal IndexPartialVM[] GetIndexPartial(int minPrice, int maxPrice, int minHK, int maxHK, string typer,
            string materials, bool isAscending, string sortOn, string search)
        {
            var cards = context.Sticks.Where(o =>
            o.Pris >= minPrice &&
            o.Pris <= maxPrice &&
            o.Hästkrafter >= minHK &&
            o.Hästkrafter <= maxHK &&
            typer.Contains(o.Kategori.Namn) &&
            materials.Contains(o.Material.Namn)
            && (string.IsNullOrEmpty(search) || o.Artikelnamn.Contains(search))).
            Select(o => new IndexPartialVM
            {
                Namn = o.Artikelnamn,
                Pris = o.Pris,
                ArtikelNr = o.Artikelnr,
            });
            IndexPartialVM[] model;
            if (isAscending)
            {
                model = cards.ToList().OrderBy(o => o.GetType().GetProperty(sortOn).GetValue(o, null)).ToArray();
            }
            else
            {
                model = cards.ToList().OrderByDescending(o => o.GetType().GetProperty(sortOn).GetValue(o, null)).ToArray();
            }

            return model;
        }

        public string GetCart()
        {
            var cookieContent = Accessor.HttpContext.Request.Cookies["ShoppingCart"];

            var shoppingCart = new List<ShoppingCartProduct>();
            shoppingCart = JsonSerializer.Deserialize<List<ShoppingCartProduct>>(cookieContent);

            return shoppingCart.First().ArtikelNr.ToString();
        }

    }
}
