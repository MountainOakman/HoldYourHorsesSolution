using HoldYourHorses.Models.Entities;

namespace HoldYourHorses.Views.Accounts
{
    public class OrderhistoryVM 
    {
        public Orderrader[] Historik { get; set; }

        public string getPriceFormatted(decimal pris)
        {
            var nfi = (System.Globalization.NumberFormatInfo)System.Globalization.CultureInfo.InvariantCulture.NumberFormat.Clone();
            nfi.NumberGroupSeparator = " ";
            return pris.ToString("#,0", nfi);
        }

    }
}
