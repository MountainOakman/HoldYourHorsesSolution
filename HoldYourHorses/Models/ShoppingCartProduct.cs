namespace HoldYourHorses.Models
{
    public class ShoppingCartProduct
    {
        List<ShoppingCartProduct> _products;

        public decimal Pris { get; set; }
        public string Artikelnamn { get; set; }
        public int Antal { get; set; }

    }
}
