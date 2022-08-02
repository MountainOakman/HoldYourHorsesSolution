using HoldYourHorses.Models.Entities;
using HoldYourHorses.Views.Sticks;

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
            var q = context.Sticks
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
                .Single()
                ;

        //TODO:Tilldela prop :public string Bild { get; set; }
            return q;
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

        internal IndexVM GetIndexVM()
        {
            var db = context.Sticks;
            var cards = db.Select(o => new Card()
            {
                Namn = o.Artikelnamn,
                Pris = o.Pris,
                ArtikelNr = o.Artikelnr,
            });
            var indexVM = new IndexVM
            {
                PrisMax = db.Max(o => o.Pris),
                PrisMin = db.Min(o => o.Pris),
                HästkrafterMax = db.Max(o => o.Hästkrafter),
                HästkrafterMin = db.Min(o => o.Hästkrafter),
                //Materialer = db.DistinctBy(o => o.Material).Select(o => o.Material).ToArray(),
                //Typer = db.DistinctBy(o => o.Typ).Select(o => o.Typ).ToArray(),
                Cards = cards.ToArray()

            };
            return indexVM;
        }

    }
}
