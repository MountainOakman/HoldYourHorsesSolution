using HoldYourHorses.Models.Entities;

namespace HoldYourHorses.Models
{
    public class DataService
    {
        private readonly SticksDBContext context;

        public DataService(SticksDBContext context)
        {
            this.context = context;
        }
    }
}
