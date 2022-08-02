namespace HoldYourHorses.Views.Sticks
{
    public class IndexVM
    {
        public Card[] Cards { get; set; } 
        public decimal PrisMin { get; set; }
        public decimal PrisMax { get; set; }
        public int HästkrafterMin { get; set; }
        public int HästkrafterMax { get; set; }
        public string[] Materialer { get; set; } 
        public string[] Typer { get; set; }


    }

    public class Card
    {
        public string Namn { get; set; }
        public decimal Pris { get; set; }
        public string Bild { get; set; }
        
        public int ArtikelNr { get; set; }
    }
}
