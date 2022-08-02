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
                .Select(o => new DetailsVM (){ Artikelnr = artikelNr })
                .Single()
                ;

            //TODO fixa resten av props
            return q;
        }
    }
}
